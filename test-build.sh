#!/bin/bash

echo "Testing Unity F# Project Build..."
echo "================================="

cd "$(dirname "$0")/GameLogic"

echo "1. Cleaning previous build..."
dotnet clean > /dev/null 2>&1

echo "2. Building F# project..."
if dotnet build --verbosity quiet; then
    echo "✅ Build successful!"
    
    echo "3. Checking output files..."
    if [ -f "../Assets/bin/GameLogic.dll" ]; then
        echo "✅ GameLogic.dll generated successfully"
        echo "   Size: $(du -h ../Assets/bin/GameLogic.dll | cut -f1)"
    else
        echo "❌ GameLogic.dll not found"
        exit 1
    fi
    
    if [ -f "../Assets/bin/FSharp.Core.dll" ]; then
        echo "✅ FSharp.Core.dll copied successfully"
    else
        echo "⚠️  FSharp.Core.dll not found (may need to be manually copied)"
    fi
    
    echo "4. Build test completed successfully! 🎉"
    echo ""
    echo "You can now:"
    echo "- Open the Unity project"
    echo "- Load the _Scenes/MiniGame.unity scene"
    echo "- Verify the F# scripts are properly attached to game objects"
    
else
    echo "❌ Build failed!"
    exit 1
fi