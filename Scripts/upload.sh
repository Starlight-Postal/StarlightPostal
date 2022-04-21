#!/bin/bash

packagefolder="PackagedRelease_$(date -I)"
tag=$(git describe --exact-match --tags)

if [ ! $tag ]; then
	echo "ERROR: This is not a tag! Refusing to upload mismatched version!"
	exit 69
fi

cd Builds/

echo "Creating and uploading release for tag: $tag"
gh release create $tag --generate-notes $packagefolder/*
echo "Done uploading"

