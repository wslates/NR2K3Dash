# NR2K3Dash

## About
NR2K3 dash is a digital dashboard that interacts with [NASCAR Racing 2003](https://en.wikipedia.org/wiki/NASCAR_Racing_2003_Season).

When looking through NR2003's files, I discovered a few C++ files that indicated the game was capable of outputting live telemetry from the game. I didn't want to deal with UI in C++, so I built an unmanaged C++ dll that outputs the data, which can then be consumed by C#. 

