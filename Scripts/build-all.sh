#!/bin/bash

if [ -f "/mnt/c/Program Files/Unity/Hub/Editor/2020.3.19f1/Editor/Unity.exe" ]; then
	unityexec="/mnt/c/Program Files/Unity/Hub/Editor/2020.3.19f1/Editor/Unity.exe"
elif [ -f "~/Unity/Hub/Editor/2020.3.19f1/Editor/Unity" ]; then
	unityexec="Unity/Hub/Editor/2020.3.19f1/Editor/Unity"
else
	echo "ERROR: Could not find Unity executable!"
	exit 1
fi

echo "Starting Unity build"

"$unityexec" -batchmode -quit -executemethod StarlightBuilder.BuildAll

echo "Unity build complete"

