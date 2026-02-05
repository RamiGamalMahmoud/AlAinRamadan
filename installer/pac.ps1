dotnet publish -c Release  ..\src\AlAinRamadan\AlAinRamadan -r win-x64 -o .\publish

$version = "0.4.5"
$pack_id = "AlAinRamadan"
$main_exe = "AlAinRamadan.exe"
$icon_path = ".\mosque.ico"
$splash_image = ".\mosque.png"
$framework = "net8.0-x64-desktop"
$pack_title = "Ain Ain Ramadan"
vpk pack -u $pack_id -v $version -p .\publish -e $main_exe --icon $icon_path --splashImage $splash_image --framework $framework --packTitle $pack_title
