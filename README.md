# Clippi-B

Steals clipboard data written in c#, executable by cobalt-strike or any other unmanaged CLR loader. 
you'll need fody NuGet package to compile.

It's intelligent enough to not spam you when the clipboard content is the same. will only display new clipboard content.

```
         .__    .__                    .__           __________
  ____   |  |   |__| ______   ______   |__|          \______   \
_/ ___\  |  |   |  | \____ \  \____ \  |  |   ______  |    |  _/
\  \___  |  |__ |  | |  |_> > |  |_> > |  |  /_____/  |    |   \
 \___  > |____/ |__| |   __/  |   __/  |__|           |______  /
     \/              |__|     |__|                           \/

developed by jfmaes / @Jean_Maes_1994
Usage:
  -i, --interval=VALUE       Intervaltimer in seconds to check if clipboard
                               has contents (default every 5 seconds)
  -m, --monitor=VALUE        How long this program should be ran in the
                               background in minutes (default 8 hours)
  -h, -?, --help             Show Help
  
```
