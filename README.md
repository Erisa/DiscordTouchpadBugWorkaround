# Discord Touchpad Bug Workaround

There exists a rare bug in the Discord desktop application that will cause its touchpad scrolling behaviour to become erratic, scrolling too fast at the wrong rates.  
I have not put much effort into finding out what causes this to happen, but it may have something to do with high DPI displays and scaling.

Some sources:
- https://www.reddit.com/r/discordapp/comments/71xw0c/discord_scroll_lagging/
- https://www.reddit.com/r/discordapp/comments/7qqwzy/windows_10_desktop_discord_app_laptop_2_finger/
- https://www.reddit.com/r/discordapp/comments/4wsudy/touchpad_scrolling_issues/

There are two main workarounds to this problem. 
- One is disabling Hardware Acceleration in Discord which is not preferred for what I hope are obvious reasons.  
- The second is resizing the Discord window in any way, even slightly. This clears up the bug until the next time the window is minimised/restored or closed/reopened.

This application takes the second solution and rolls with it. When the application detects that you have Discord focused, it will resize the window by 1px and then back again the next second. On almost all high DPI displays you will not notice this and it will not have any negative repurcussions besides having another process in the background. However, this is a hacky workaround so there may be some edge cases and race conditions that cause it to not work correctly.

Note that this method will not work if you have Discord maximised. I don't personally maximise the Discord window so this wasn't a concern for me, and I'm not completely sure how this silent workaround would even work if the windows is maximised. If you have any ideas, feel free to file an Issue with them.

## Usage

To use the application, please first read and understand the following paragraph:
```
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
```

With that out of the way, grab a [released binary](https://github.com/Erisa/DiscordTouchpadBugWorkaround/releases) and run it.  
There is currently no user interface of any kind, the process will simply run in the background silently. To exit it, you will currently have to locate it on Task Manager. Maybe in future I might add a tray icon?

Once you have verified this improves Discord touchpad scrolling performance for you, drop it in the Startup directory (Navigate to `shell:startup`) and you should be good to go.

## Credits
While the main logic of this was written by myself, like any developer I did some research and some ideas or code come from StackOverflow answers.  

For transparency, these are:
- https://stackoverflow.com/a/6569555
- https://stackoverflow.com/a/6415255
- https://stackoverflow.com/a/2686409
