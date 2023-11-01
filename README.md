<h1>Thread On Me 🐍</h1>
Hi! This is the guide on programming with this esolang. To do stuff, simply download the interpreter and execute it in command line along with a text file with the program on it. If there are any questions, just contact me on discord at Leol22, i have nothing to do.
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
finally <br>For the letter commands, it's implied that you can use the upper case version to interact upwards.
<table>
  <tr>
    <th>Command</th>
    <th>Purpose</th>
  </tr>
    <tr>
    <th colspan="2">Basics</th>
  </tr>
  <tr>
    <td>.</td>
    <td>No-op. Will still take a "turn" to execute, making it useful to better sync threads.</td>
  </tr>
  <tr>
    <td>1/2/3/4/5</td>
    <td>Will swap the acc for the corresponding swap-storage. This is the only way to set/read from the swap storages. If you've ever played TIS-100, this is the BAK register.</td>
  </tr>
  <tr>
    <td>+</td>
    <td>Will increase the acc by 1.</td>
  </tr>
  <tr>
    <td>-</td>
    <td>Will decrease the acc by 1.</td>
  </tr>
  <tr>
    <td>=</td>
    <td>Will halt the program.</td>
  </tr>
  <tr>
    <td>:</td>
    <td>Will kill the thread it's executed on, and remove it from the program. If a program has no threads, it will automatically halt.</td>
  </tr>
    <tr>
    <td>k</td>
    <td>Will kill the line above or below, works both with threads and with arrays.</td>
  </tr>
  <tr>
    <td>^</td>
    <td>Will output the acc as an int. If the symbol doesn't make sense blame LyricLy.</td>
  </tr>
  <tr>
    <td>"</td>
    <td>Will output the acc as an char.</td>
  </tr>
  <tr>
    <td>p</td>
    <td>Oh boy, this one is gonna take a while. P, or Push, will send the current value of the acc upwards or downwards. If "upwards or downwards" is an array, then it will write that value to the array, overriding whatever was there before. If it's a thread, then it will queue it for the next mathematical operation (don't worry we'll look at them later). <br>The important thing to remember is that the queue is placed on the receiving thread, not the sending one. This means that if the sending thread gets killed before the data is read the data still remains. Three important things:<br>1: after pushing, the thread will NOT stop to wait for a read, it will simply continue.<br>2: if a thread pushes to a queue with a value already in it, it will be overriden.<br>3: every cell has two queues, one from above and one from below.<br>Sorry for the massive infodump.</td>
  </tr>
    <tr>
    <th colspan="2">Math & Stuff</th>
  </tr>
<tr>
  <td>c</td>
  <td>This will take the value from above or below and put it in the acc. If it's from an array, it will simply read it. If not, it works with the P command we talked about earlier. If a value is not available, the program continues on until it is.
  </td>
</tr>
<tr>
  <td>a</td>
  <td>Same as C, but will add the value to the current acc.</td>
</tr>
  <tr>
  <td>s</td>
  <td>Same as C, but will subtract the value to the current acc.</td>
</tr>
  <tr>
  <td>m</td>
  <td>Same as C, but will multiply the value with the current acc (and store the result there).</td>
</tr>
  <tr>
  <td>d</td>
  <td>Same as C, but will divide the acc by the value (result will be floored)</td>
</tr>
  <tr>
  <td>r</td>
  <td>Same as C, but will calculate Acc%Value (modulo or remainder) and store it in Acc</td>
</tr>
<tr>
  <td>l</td>
  <td>Will set the acc to the lenght of the above/below line. If it's a thread, it will take the number of instructions. If it's an array, the number of items. This is useful to do work on arrays with sequences you don't know the lenght of.</td>
</tr>
  <tr>
    <td>b/f</td>
    <td>Will move the above/below array's pointer backwards or forwards by 1.</td>
  </tr>
<tr>
  <td>j</td>
  <td>Will wait for the cell above or below to use a "j" command. Remember to use the properly capitalized j for BOTH threads, as you want them "pointing" to each other.</td>
</tr>
  <tr>
    <th colspan="2">Replication</th>
  </tr>
<tr>
  <td>#</td>
  <td>Any code after this will not be run, the thread will just start from the beginning. Made to be used with "y".</td>
</tr>
<tr>
  <td>y</td>
  <td>Probably the most important command, "y" will create a thread below or above. This thread will have the same code as the parent, but will start at the first command and all the registers will be 0. HOWEVER, if a "#" command is present the thread will instead have as code whatever is placed after the first #. A second "#" indicates sub-sub children code, and so on.</td>
</tr>
    <tr>
    <th colspan="2">Conditionals</th>
  </tr>
  <tr>
    <td><></td>
      <td>IF 0 statement, will skip to the closer character if acc is not zero.</td>
  </tr>
    <tr>
    <td>()</td>
      <td>IF NOT 0 statement, will skip to the closer character if acc is zero.</td>
  </tr>
      <tr>
    <td>[]</td>
      <td>UNTIL 0 statement, will repeat the sequence until acc is 0 (like in brainfuck!)</td>
  </tr>
        <td>{}</td>
      <td>UNTIL NOT 0 statement, like [], but reversed condition.</td>
  </tr>
</table>
      Phew, that's a lot! One final note, lines starting with "/" will be counted as comments.
<h2>Quirks</h2>
      This language has a few quirks:
      <ol>
        <li>Upward push skip: technically speaking, if a "c" command is perfecty syncronised with a "P", the "c" will not find any value, and then the "P" pushes up, this means that this operation will take a full cycle. This is unintuitive, so i added in that if a situation like this occurs it will simply do the thing immediately, and then skip the push instruction.</li>
        <li>Conditional commands are still commands! That means that they will take a "turn" to complete. BUT if the condition is not met, the program will skip to the command AFTER the closer, thus skipping it.</li>
        <li>There's nothing technically stopping conditionals from intersecting like [{]}, but i have 0 clue how stuff would operate. Try at your own risk. Under the hood, conditionals are just fancy jump commands, so make of that what you will.</li>
      </ol>
<h2>Example Programs</h2>
coming soon i'm tired
