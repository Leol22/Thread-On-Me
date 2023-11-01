hi this readme still has to be finished
<h1>Thread On Me 🐍</h1>
<h2>Introduction</h2>
Before we start with the funny, you must understand how this language works. Basically, every line is a thread and every command is a character. If that's confusing, i don't blame you, i'm bad at explaining things. Here's what a program could look like:<br>
<pre><code>qwer</code><br><code>tyu</code><br><code>io</code></pre> Code is executed left to right, in parallel for every thread. Threads will loop indefinately, so this program will execute commands in this order: "qtiwyoeuirtoqyiwuo" and so on. Obviously, threads will NOT loop forever, they will halt when told.
<h2>Registers & Comunication</h2>
Every thread has an internal storage, comprised of an accumulator register and 5 of what i call "swap storages". Don't worry about those for now. Commands in this language can be one of two types. Either symbols, or letters. Symbols are commands that are only relevant for the thread itself, meanwhile letters are commands that in some way influence other threads or that influence their thread based on others. The reason for this distinction is that threads can only ever comunicate with stuff directly above or below them! Letters were chosen because they can be either uppercase (influence the thread above) or lower case (influence the thread below). There is one exception to this rule, and that is "=", the command that halts the entire program. This is because the command does not require any particular direction, it just stops the program.
<h2>Arrays</h2>
<h2>Command List</h2>
<h2>Quirks</h2>
<h2>Example Programs</h2>
