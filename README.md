# Minesweeper

This is an implementation of a classic Minesweeper game.
You can set the layout yourself.
Rules for the setup:
- at least 10 fields size in both width and height
- at least two third of the fields must not be mines
- it is recommended (though not required) to not have more than 35 fields in height and 70 fields in width

The game:
- Search for mines hidden on the game display.
- Left mouseclick on a field will clean the field. If it is a mine, you lose. If it is a number, it indicates how many mines surround that field.
- Right mouseclick will mark or unmark a field as suspected mine. A field has to be unmarked to be cleaned.
- If you clean a field with no surrounding mines, all surrounding fields are cleaned automatically.
- If you clean all fields that are not mines, you win

## Version 1.0
Features:
- Added "hint" button and cheat button (to see all possible hints).
- Cheat is the left "Shift" key + F12
- Only "direct" hints are revealed, based on certain mines in the field, not based on any "1 out of 2"-calculations or anything more complex. So after all, you should be better than the hint/cheat assistant

## Version 1.0.1
Features:
- Setup window closing now is equal to pressing the "Start Game" button
- Minimum value for mines (5) is regarded
- Magic numbers from the setting window moved to Constants

## Version 1.0.2
Features:
- Minimum value for width and height is regarded
- "New settings" button added

## Version 1.0.3
Features:
- Minimum width changed
- Layout "settings"-Menu improved
- Moved buttons to Menu
- Timer and unmarked mines counter added