# NR2K3Dash

## About
NR2K3 dash is a digital dashboard that interacts with [NASCAR Racing 2003](https://en.wikipedia.org/wiki/NASCAR_Racing_2003_Season).

When looking through NR2003's files, I discovered a few C++ files that indicated the game was capable of outputting live telemetry from the game. I didn't want to deal with UI in C++, so I built an unmanaged C++ dll that outputs specifically outputs the game's gauge telemetry, which can then be consumed by C#.

I built all the UI elements myself using Adobe Illustrator.

## Progress
I have been detailing my progress on this project on Youtube to show to the NR2003 modding community.
- [Initial Research](https://youtu.be/PCQH0ikYatI?t=48)
-- This was my initial research into getting the data from the game. This was simply a C++ console app that would constantly update with the current gauge data.
- [Initial UI Implementation](https://youtu.be/PIa7NRZof-0?t=36)
-- This was a video detailing my first UI implementation.


## Known Issues
- Program needs to be run in administrator in order to allow for keyboard hooks to work. I've read that this is an issue with DirectX, and have since been researching how to solve it. (I don't want to make this necessary to run as Administrator.)
- There seems to be a memory leak somewhere. C#'s garbage collector doesn't seem to be picking up view models when they are not being used. I have not been able to pinpoint anywhere in code where there are some references being kept. It might also have something to do with the usage of "pages." I am still tracking down the source of this issue.
- Very little documentation at the moment, which is something I am working on. When I began this, it was simply a proof of concept that it worked, and I was just trying to get something to work!


## Planned Improvements
- Improve the abstraction of pages. The software has multiple "pages", i.e. different "themes" of how the dashboard looks. I do not like the current abstraction of these in code - each page needs it's own separate class and viewmodel, so I am working on developing better abstractions.
- Get more data from the game. These digital dashboards actually report a lot more than just the RPM, temperatures, and pressures. I am currently researching exactly what data these dashboards are capable of representing, and how I would be able to extract those from values from the game.

## Credits
The UI of this is based on McClaren's digital dashboard.

