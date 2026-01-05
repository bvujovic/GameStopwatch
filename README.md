# GameStopwatch

Stopwatches/timers are defined by key(press), time and sound that is played.
On keypress event timer is started and after specified time the sound (voice command) will be heard.
That way player can be prepared to collect items that appear on some schedule.
Time spent in game is shown in table and chart form.

![Game Stopwatch - Main Window](ScreenShots/MainForm.png)

![Game Stopwatch - Past Values (statistics)](ScreenShots/FrmPastValues.png)

## TODO
- [x] Change .NET version 8->10 of the main project. Add projects: BvWinFormsLib and Tests (xunit).
- [x] Save&Load main window position
- [x] Bugfix: Main window doesn't display current data when I exit the game. Try calling Display...() on Form.Active or similar event.
- [x] Notify user if the battery on laptop is less then xy%
- [ ] Code refactoring
