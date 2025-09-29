@echo off
setlocal enabledelayedexpansion

set "output_file=combined_cs_files.txt"
set "root_dir=."

echo Creating combined file: %output_file%

if exist "%output_file%" (
    del "%output_file%"
)

for /r "%root_dir%" %%f in (*.cs) do (
    set "file_path=%%f"
    set "relative_path=!file_path:%cd%\=!"
    
    echo. >> "%output_file%"
    echo ======================================== >> "%output_file%"
    echo File: !relative_path! >> "%output_file%"
    echo ======================================== >> "%output_file%"
    echo. >> "%output_file%"
    
    type "%%f" >> "%output_file%"
    echo. >> "%output_file%"
)

echo Done! Check %output_file%