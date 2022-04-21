#!/bin/bash

packagefolder="PackagedRelease_$(date -I)"
tag=$(git describe --exact-match --tags)

if [ ! $tag ]; then
	echo "ERROR: This is not a tag! Refusing to upload mismatched version!"
	exit 69
fi

cd Builds/

echo "Uploading tag: $tag"
gh release upload $tag $packagefolder/*
echo "Done uploading"

