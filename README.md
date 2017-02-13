### Word counter

This is a program that hooks the keyboard at a low level thanks to [this library](http://www.dylansweb.com/2014/10/low-level-global-keyboard-hook-sink-in-c-net/).

If a letter key is pressed, it is appended to a string `current_word`. When the space or enter keys are pressed, the `current_word` is checked against all subscribed words, and if it matches any words, that word's counter is incremented by one. The `current_word` value is reset to `""`, ready to parse the next word.

Keyboard shorts

| Combination   | Function                           |
|:-------------:|:-----------------------------------|
| F2 + F3       | Show statistics for words          |
| F2 + F4       | Show statistics for words and exit |
| F2 + F5       | Add a subscription                 |

By default, the word "like" is tracked, as that was my original intent: To see how many times I typed the word "like" every day.

---

##### Technical
This program has a small window that is pushed off the desktop. If the program didn't have this window, keyboard hooking wouldn't work, and I couldn't hide the form without the program terminating. Don't ask me, I have no clue why Windows behaves like that.
