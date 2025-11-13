Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  适用于 FFmpegFreeUI 的输出文件时间戳清理工具" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 提示输入路径
Write-Host "请输入要处理的文件夹路径" -ForegroundColor Yellow
Write-Host "（直接按回车使用当前目录）:" -ForegroundColor Gray
$inputPath = Read-Host "路径"

# 如果用户没有输入，使用当前目录
if ([string]::IsNullOrWhiteSpace($inputPath)) {
    $inputPath = Get-Location
    Write-Host "使用当前目录: $inputPath" -ForegroundColor Green
}

# 验证路径是否存在
if (-not (Test-Path -Path $inputPath)) {
    Write-Host ""
    Write-Error "路径不存在: $inputPath"
    Write-Host "按任意键退出..." -ForegroundColor Gray
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 1
}

Write-Host ""

# 询问是否递归处理子文件夹
Write-Host "是否递归处理子文件夹? (Y/N)" -ForegroundColor Yellow
$recursiveInput = Read-Host "选择"
$isRecursive = $recursiveInput -match '^[Yy]'

Write-Host ""

# 询问是否预览模式
Write-Host "是否启用预览模式（只查看不修改）? (Y/N)" -ForegroundColor Yellow
$whatIfInput = Read-Host "选择"
$isWhatIf = $whatIfInput -match '^[Yy]'

Write-Host ""
Write-Host "========================================" -ForegroundColor Gray

# 时间戳正则表达式模式: _YYYY.MM.DD-HH.MM.SS
$timestampPattern = '_\d{4}\.\d{2}\.\d{2}-\d{2}\.\d{2}\.\d{2}'

# 获取文件列表
$getChildItemParams = @{
    Path = $inputPath
    File = $true
}

if ($isRecursive) {
    $getChildItemParams['Recurse'] = $true
}

Write-Host "开始扫描文件..." -ForegroundColor Cyan
$files = Get-ChildItem @getChildItemParams

# 统计信息
$totalFiles = 0
$renamedFiles = 0
$skippedFiles = 0
$errorFiles = 0

Write-Host "路径: $((Resolve-Path $inputPath).Path)" -ForegroundColor Gray
Write-Host "递归: $isRecursive" -ForegroundColor Gray
Write-Host "预览模式: $isWhatIf" -ForegroundColor Gray
Write-Host ("-" * 80) -ForegroundColor Gray

foreach ($file in $files) {
    $totalFiles++
    $oldName = $file.Name
    
    # 检查文件名是否包含时间戳
    if ($oldName -match $timestampPattern) {
        # 移除时间戳
        $newName = $oldName -replace $timestampPattern, ''
        
        # 构建新的完整路径
        $newPath = Join-Path -Path $file.DirectoryName -ChildPath $newName
        
        # 检查新文件名是否已存在
        if ((Test-Path -Path $newPath) -and ($file.FullName -ne $newPath)) {
            Write-Warning "跳过: $oldName -> $newName (目标文件已存在)"
            $skippedFiles++
            continue
        }
        
        try {
            if ($isWhatIf) {
                Write-Host "[预览] " -ForegroundColor Yellow -NoNewline
                Write-Host "$oldName -> $newName" -ForegroundColor White
            } else {
                Rename-Item -Path $file.FullName -NewName $newName -ErrorAction Stop
                Write-Host "[已重命名] " -ForegroundColor Green -NoNewline
                Write-Host "$oldName -> $newName" -ForegroundColor White
            }
            $renamedFiles++
        }
        catch {
            Write-Error "重命名失败: $oldName - $_"
            $errorFiles++
        }
    }
}

# 输出统计信息
Write-Host ("-" * 80) -ForegroundColor Gray
Write-Host "处理完成!" -ForegroundColor Cyan
Write-Host "总文件数: $totalFiles" -ForegroundColor White
Write-Host "匹配时间戳的文件: $renamedFiles" -ForegroundColor Cyan

if ($isWhatIf) {
    Write-Host "预览的重命名: $renamedFiles" -ForegroundColor Yellow
} else {
    Write-Host "已重命名: $renamedFiles" -ForegroundColor Green
}

if ($skippedFiles -gt 0) {
    Write-Host "已跳过: $skippedFiles" -ForegroundColor Yellow
}
if ($errorFiles -gt 0) {
    Write-Host "失败: $errorFiles" -ForegroundColor Red
}

Write-Host ""

if ($isWhatIf -and $renamedFiles -gt 0) {
    Write-Host "这是预览模式。要实际执行重命名，请重新运行脚本并选择 N (不启用预览模式)。" -ForegroundColor Yellow
}

# 等待用户按键退出
Write-Host ""
Write-Host "按任意键退出..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")