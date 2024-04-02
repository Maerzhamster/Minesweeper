# Minesweeper

This is an implementation of a classic Minesweeper game.
You can set the layout yourself.
Rules for the setup
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
- Cheat button is the left "Shift" button + F12
- Ónly "direct" hints are revealed, based on certain mines in the field, not based on any "1 out of 2"-calculations or anything more complex. So after all, you should be better than the cheat assistant
