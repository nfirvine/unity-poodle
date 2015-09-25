unity-poodle
============

Demonstrates that Unity's System.Net.HttpWebRequest is vulnerable to POODLE in its default configuration.

Mitigation is to disable SSLv3 in lieu of enabling TLS_FALLBACK_SCSV (not supported). That leaves TLS1.0, which is more secure, at least as secure as we'll get until TLS1.1+ are supported. 

Usage
-----

Start the scene and watch the console.
