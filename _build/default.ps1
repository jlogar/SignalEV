properties {
    $root = resolve-path .
    $tools = resolve-path $root\..\_tools
    
    $sln_file = resolve-path "$root\..\SignalEV.sln"
    $project_file = resolve-path "$root\..\SignalEV\SignalEV.csproj"
}

task default -Depends build

task build {
    exec { msbuild $sln_file "/p:Configuration=Release" }
}