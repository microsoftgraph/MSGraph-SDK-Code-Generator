msbuild Typewriter.sln /p:Configuration=Release /m
choco pack Typewriter.nuspec -version %1
choco upgrade Typewriter -s .