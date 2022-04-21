#!/bin/bash

cd Builds

packagefolder="PackagedRelease_$(date -I)"
echo "Creating package folder at Builds/$packagefolder"
mkdir $packagefolder

if [ -d "StarlightPostal-windows-x86_64" ]; then
	echo "Compressing Windows 64-bit build"
	zip -qr $packagefolder/StarlightPostal-windows-x86_64.zip StarlightPostal-windows-x86_64
else
	echo "WARNING: Windows build not found!"
fi

if [ -d "StarlightPostal-linux-x86_64" ]; then
	echo "Compressing Linux 64-bit build"
	tar -czf $packagefolder/StarlightPostal-linux-x86_64.tar.gz StarlightPostal-linux-x86_64
else
	echo "WARNING: Linux build not found!"
fi

if [ -d "StarlightPostal-macos" ]; then
	echo "Packaging MacOS build"
	genisoimage -quiet -V StarlightPostal -D -R -apple -no-pad -o $packagefolder/StarlightPostal-macos-uncompressed.dmg StarlightPostal-macos
	echo "Compressing MacOS image"
	dmg $packagefolder/StarlightPostal-macos-uncompressed.dmg $packagefolder/StarlightPostal-macos.dmg
	echo "Removing uncompressed MacOS image"
	rm $packagefolder/StarlightPostal-macos-uncompressed.dmg
else
	echo "WARNING: MacOS build not found!"
fi

if [ -d "StarlightPostal-webgl" ]; then
	echo "Compressing WebGL build"
        zip -qr $packagefolder/StarlightPostal-webgl.zip StarlightPostal-webgl
else
	echo "WARNING: WebGL build not found!"
fi

if [ -f "StarlightPostal-android.apk" ]; then
	echo "Copying Android build"
	cp StarlightPostal-android.apk $packagefolder/StarlightPostal-android.apk
else
	echo "WARNING: Android build not found!"
fi

cd $packagefolder

echo "Calculating SHA-256 sums"
sha256sum * > checksums.sha256.txt

echo "Done!"

