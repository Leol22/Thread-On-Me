hi this readme still has to be finished
<h1>Thread On Me 🐍</h1>
<h2>Introduction</h2>
Before we start with the funny, you must understand how this language works. Basically, every line is a thread and every command is a character. If that's confusing, i don't blame you, i'm bad at explaining things. Here's what a program could look like:<br>
<pre><code>qwer</code><br><code>tyu</code><br><code>io</code></pre> Code is executed left to right, in parallel for every thread. Threads will loop indefinately, so this program will execute commands in this order: "qtiwyoeuirtoqyiwuo" and so on. Obviously, threads will NOT loop forever, they will halt when told.
<h2>Registers & Comunication</h2>
Every thread has an internal storage, comprised of an accumulator register and 5 of what i call "swap storages". Don't worry about those for now.<br> Commands in this language can be one of two types. Either symbols, or letters. Symbols are commands that are only relevant for the thread itself, meanwhile letters are commands that in some way influence other threads or that influence their thread based on others.<br><br> The reason for this distinction is that threads can only ever comunicate with stuff directly above or below them! Letters were chosen because they can be either uppercase (influence the thread above) or lower case (influence the thread below). There is one exception to this rule, and that is "=", the command that halts the entire program. This is because the command does not require any particular direction, it just stops the program.
<h2>Arrays</h2>
The last piece of theory we have to go over before we can get to the fun stuff are arrays.<br>
Arrays weren't mentioned before because they break the "1 line 1 thread" rule and i didn't want to ruin the catchphrase, but they are a pretty important part of the lang!<br>
An array is any line that starts with "!", and whatever follows are the content of the array. Once an array is declared at program start, their size is fixed, but the contents can be modified. <br> Every array has a pointer, every time it is read from or wrote to the pointer increases by one. The pointer wraps around then it reaches the end of the arrays, and there are obviously ways to move it backwards, which we'll get to later.<br>Arrays are also important because they are the only way to get inputs!<br>Now, let's look at what you can put in an array.<br>
<table>
  <tr>
    <th>Symbol</th>
    <th>Purpose</th>
  </tr>
  <tr>
    <td>-</td>
    <td>Will add an empty cell to the array, initialized at 0</td>
  </tr>
    <tr>
    <td>_</td>
    <td>Will add a single int input to the array.</td>
  </tr>
    <tr>
    <td>)</td>
    <td>Will take a sequence of ints as an input, this can be any lenght the user needs.</td>
  </tr>
    </tr>
    <tr>
    <td>]</td>
    <td>Will add a single char input to the array. (converted to int)</td>
  </tr>
    <tr>
    <td>}</td>
    <td>Will take a sequence of chars as an input, this can be any lenght the user needs.</td>
  </tr>
  <tr>
    <td>"</td>
    <td>Will add the number between quotation marks to the array, so for example "10" will add 10 to the array.</td>
  </tr>
</table>
Remember that you can mix and match! All these are valid arrays:
<pre><code>!)__</code><br><code>!}"10"}</code><br><code>!"69"-"420"_)</code></pre>
<h2>Command List</h2>
<h2>Quirks</h2>
<h2>Example Programs</h2>
