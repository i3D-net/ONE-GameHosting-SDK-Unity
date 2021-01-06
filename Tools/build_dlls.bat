@REM This script runs the libraries building sequense on the SDK side.
@REM It requires Docker installed. The path to the SDK repository has to be provided as the argument.

SET DllsPath="..\ONE SDK Plugin\Assets\Plugins\i3D\"
SET BuildBatchDir="Tools"
SET BuildBatch="build_release_dlls.bat"
SET OutputDir=shared_lib_build

IF [%1]==[] (
    echo "SDK path should be provided"
) ELSE (
	cd %1\%BuildBatchDir%
    CALL .\%BuildBatch%
    xcopy %OutputDir% %~dp0\%DllsPath% /s /y
)