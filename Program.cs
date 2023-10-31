string path = @$"./{args[0]}";
string[] rawcode = File.ReadAllLines(path);
List<bool> array= new();
List<string> lines = new();
List<int[]> arrays= new List<int[]>();
List<int[]> registers= new();
/*
 Types=
True:Thread
False:Array

 Registers=
0=Acc (and array pointer)
1-5=Swap storage
6=Pointer
7=Top Queue
8=Bottom Queue

 */
//CLEANUP
int arraycounter = 0;
foreach(string line in rawcode)
{
	if (line[0] == '!')
	{
		//array definition
		int arraytempcounter = 1;
		List<int> temparrayl= new List<int>();
		while (arraytempcounter < line.Length)
		{
			if (line[arraytempcounter]=='_') 
			{
			Console.WriteLine("Int input:");
			temparrayl.Add(int.Parse(Console.ReadLine()));
			}
			if (line[arraytempcounter] == ']')
			{
				Console.WriteLine("Char input:");
				temparrayl.Add(Console.ReadLine()[0]);
			}
			if (line[arraytempcounter] == '-')
			{
				temparrayl.Add(0);
			}
			if (line[arraytempcounter] == ')')
			{
				Console.WriteLine("Int sequence (end by sending an empty line):");
				string iinput= Console.ReadLine();
				while (iinput.Length > 0)
				{
					temparrayl.Add(int.Parse(iinput));
					iinput = Console.ReadLine();
				}
			}
			if (line[arraytempcounter] == '}')
			{
				Console.WriteLine("Char sequence (end by sending an empty line):");
				string cinput = Console.ReadLine();
				while (cinput.Length > 0)
				{
					temparrayl.Add(cinput[0]);
					cinput = Console.ReadLine();
				}
			}
			if (line[arraytempcounter] == '"')
			{
				string toarrayout = "";
				arraytempcounter++;
				char tout = line[arraytempcounter];
				while (tout != '"')
				{
					toarrayout += tout;
					arraytempcounter++;
					tout = line[arraytempcounter];

				}
				temparrayl.Add(int.Parse(toarrayout));
			}

			arraytempcounter++;
		}
		int[] temparray = new int[temparrayl.Count];
		for (int i=0;i<temparrayl.Count;i++)
		{
			temparray[i] = temparrayl[i];
		}
		array.Add(true);
		lines.Add(arraycounter.ToString());
		arrays.Add(temparray);
		registers.Add(new int[1] {0});
	}
	else
	{
		if (line[0] != '/')
		{
			//thread definition
			array.Add(false);
			lines.Add(line);
			registers.Add(new int[9] { 0,0,0,0,0,0,0,0,0 }) ;
		}
	}
}
//function
int skip = 0;
int getposition(int point, bool direction)
{
	//QUEUES MUST END WITH 1 TO BE VALID
	if (direction)
	{
		if (registers[point][7] % 10 == 1)
		{
			int ret = registers[point][7];
			registers[point][7] = 0;
			return ret;
		}
		else
		{
			if (point > 0 && array[point - 1])
			{
				int aim = int.Parse(lines[point - 1]);
				int ret= arrays[aim][registers[point-1][0]]*10+1;
				registers[point - 1][0]++;
				if (registers[point - 1][0] == arrays[aim].Length)
				{
					registers[point - 1][0]=0;
				}
				return ret;
			}
			else { return 0; }
		}
	}
	else
	{
		if (registers[point][8] % 10 == 1)
		{
			int ret = registers[point][8];
			registers[point][8] = 0;
			return ret;
		}
		else
		{
				if (point < lines.Count - 1 && array[point + 1])
				{
					int aim = int.Parse(lines[point + 1]);
					int ret = arrays[aim][registers[point + 1][0]] * 10 + 1;
					registers[point + 1][0]++;
					if (registers[point + 1][0] == arrays[aim].Length)
					{
						registers[point + 1][0] = 0;
					}
					return ret;
				}
				else { 
				if (point<lines.Count - 1 && lines[point + 1][registers[point + 1][6]] == 'P')
				{
					skip = 1;
					registers[point + 1][6]++;
					if (registers[point + 1][6] == lines[point].Length || lines[point+1][registers[point + 1][6]] == '#')
					{
						registers[point + 1][6] = 0;
					}
					registers[point][8] = 0;
					return registers[point + 1][0] * 10 + 1;
				}
				else
				{
					return 0;
				}
			}
			
		}
	}
	return 0;
}
//RUNTIME
bool active = true;
int pointer = 0;

while (active)
{
	if (!array[pointer])
	{
		if (skip > 0)
		{
			skip--;
		}
		else
		{
			char instruction = lines[pointer].ToLower()[registers[pointer][6]];
			bool upper = (instruction != lines[pointer][registers[pointer][6]]);
			//Console.WriteLine($"{pointer}  {lines[pointer]}  {registers[pointer][0]}   {instruction}");
			int stemp;
			switch (instruction)
			{
				//sync
				case 'j':
					if (upper)
					{
						if (pointer > 0 && !array[pointer - 1] && lines[pointer - 1][registers[pointer - 1][6]] == 'j')
						{
							registers[pointer - 1][6]++;
							if (registers[pointer - 1][6] == lines[pointer - 1].Length || lines[pointer - 1][registers[pointer - 1][6]]=='#')
							{
								registers[pointer - 1][6] = 0;
							}
						}
						else { registers[pointer][6]--; }
					}
					else
					{
						if (pointer < lines.Count-1 && !array[pointer + 1] && lines[pointer + 1][registers[pointer + 1][6]] == 'J')
						{
							registers[pointer + 1][6]++;
							if (registers[pointer + 1][6] == lines[pointer + 1].Length || lines[pointer + 1][registers[pointer + 1][6]]=='#')
							{
								registers[pointer + 1][6] = 0;
							}
						}
						else { registers[pointer][6]--; }
					}

					break;
				case 'l':
					if (upper)
					{
						if (pointer > 0)
						{
							if (array[pointer - 1])
							{
								registers[pointer][0] = arrays[int.Parse(lines[pointer - 1])].Length;
							}
							else
							{
								registers[pointer][0] = lines[pointer - 1].Length;
							}
						}
					}
					else
					{
						if (pointer < lines.Count-1)
						{
							if (array[pointer + 1])
							{
								registers[pointer][0] = arrays[int.Parse(lines[pointer + 1])].Length;
							}
							else
							{
								registers[pointer][0] = lines[pointer + 1].Length;
							}
						}
					}
					break;
				//manip
				case '1':
					stemp = registers[pointer][0];
					registers[pointer][0] = registers[pointer][1];
					registers[pointer][1] = stemp;
					break;
				case '2':
					stemp = registers[pointer][0];
					registers[pointer][0] = registers[pointer][2];
					registers[pointer][2] = stemp;
					break;
				case '3':
					stemp = registers[pointer][0];
					registers[pointer][0] = registers[pointer][3];
					registers[pointer][3] = stemp;
					break;
				case '4':
					stemp = registers[pointer][0];
					registers[pointer][0] = registers[pointer][4];
					registers[pointer][4] = stemp;
					break;
				case '5':
					stemp = registers[pointer][0];
					registers[pointer][0] = registers[pointer][5];
					registers[pointer][5] = stemp;
					break;
				case 'p':
					if (upper)
					{
						if (pointer > 0)
						{
							if (array[pointer-1])
							{
								arrays[int.Parse(lines[pointer-1])][registers[pointer - 1][0]] = registers[pointer][0];
								registers[pointer -1][0]++;
								if (registers[pointer - 1][0] == arrays[int.Parse(lines[pointer - 1])].Length)
								{
									registers[pointer - 1][0] = 0;
								}
							}
							else
							{
								registers[pointer - 1][8] = registers[pointer][0] * 10 + 1;
							}
						}
					}
					else
					{
						if (pointer < lines.Count-1)
						{
							if (array[pointer + 1])
							{
								arrays[int.Parse(lines[pointer + 1])][registers[pointer + 1][0]] = registers[pointer][0];
								registers[pointer + 1][0]++;
								if (registers[pointer + 1][0] == arrays[int.Parse(lines[pointer + 1])].Length)
								{
									registers[pointer + 1][0] = 0;
								}
							}
							else
							{
								registers[pointer + 1][7] = registers[pointer][0] * 10 + 1;
							}
						}
					}
					break;
				case 'b':
					if (upper)
					{
						if(pointer>0 && array[pointer - 1])
						{
							registers[pointer - 1][0]--;
							if (registers[pointer - 1][0] == -1)
							{
								registers[pointer - 1][0] = arrays[int.Parse(lines[pointer-1])].Length-1;
							}	
						}
					}
					else
					{
						if (pointer < lines.Count-1 && array[pointer + 1])
						{
							registers[pointer + 1][0]--;
							if (registers[pointer + 1][0] == -1)
							{
								registers[pointer + 1][0] = arrays[int.Parse(lines[pointer + 1])].Length-1;
							}
						}
					}
					break;
				case 'f':
					if (upper)
					{
						if (pointer > 0 && array[pointer - 1])
						{
							registers[pointer - 1][0]++;
							if (registers[pointer - 1][0] == arrays[int.Parse(lines[pointer - 1])].Length)
							{
								registers[pointer - 1][0] = 0;
							}
						}
					}
					else
					{
						if (pointer < lines.Count - 1 && array[pointer + 1])
						{
							registers[pointer + 1][0]++;
							if (registers[pointer + 1][0] == arrays[int.Parse(lines[pointer - 1])].Length)
							{
								registers[pointer + 1][0] = 0;
							}
						}
					}
					break;
				//arr
				case '+':
					registers[pointer][0]++;
					break;
				case '-':
					registers[pointer][0]--;
					break;
				//logic
				case '<':
					if (registers[pointer][0] !=0)
					{
						int ifbracketcount=1;
						while(ifbracketcount > 0)
						{
							registers[pointer][6]++;
							if (lines[pointer][registers[pointer][6]] == '<')
							{
								ifbracketcount++;
							}
							if (lines[pointer][registers[pointer][6]] == '>')
							{
								ifbracketcount--;
							}
						}
					}
					break;
				case '(':
					if (registers[pointer][0] == 0)
					{
						int aifbracketcount = 1;
						while (aifbracketcount > 0)
						{
							registers[pointer][6]++;
							if (lines[pointer][registers[pointer][6]] == '(')
							{
								aifbracketcount++;
							}
							if (lines[pointer][registers[pointer][6]] == ')')
							{
								aifbracketcount--;
							}
						}
					}
					break;
				case '[':
					if (registers[pointer][0] == 0)
					{
						int loopbracketcount = 1;
						while (loopbracketcount > 0)
						{
							registers[pointer][6]++;
							if (lines[pointer][registers[pointer][6]] == '[')
							{
								loopbracketcount++;
							}
							if (lines[pointer][registers[pointer][6]] == ']')
							{
								loopbracketcount--;
							}
						}
					}
					break;
				case ']':
					if (registers[pointer][0] != 0)
					{
						int loopbracketcount = 1;
						while (loopbracketcount > 0)
						{
							registers[pointer][6]--;
							if (lines[pointer][registers[pointer][6]] == ']')
							{
								loopbracketcount++;
							}
							if (lines[pointer][registers[pointer][6]] == '[')
							{
								loopbracketcount--;
							}
						}
					}
					break;
				case '{':
					if (registers[pointer][0] != 0)
					{
						int aloopbracketcount = 1;
						while (aloopbracketcount > 0)
						{
							registers[pointer][6]++;
							if (lines[pointer][registers[pointer][6]] == '{')
							{
								aloopbracketcount++;
							}
							if (lines[pointer][registers[pointer][6]] == '}')
							{
								aloopbracketcount--;
							}
						}
					}
					break;
				case '}':
					if (registers[pointer][0] == 0)
					{
						int aloopbracketcount = 1;
						while (aloopbracketcount > 0)
						{
							registers[pointer][6]--;
							if (lines[pointer][registers[pointer][6]] == '}')
							{
								aloopbracketcount++;
							}
							if (lines[pointer][registers[pointer][6]] == '{')
							{
								aloopbracketcount--;
							}
						}
					}
					break;
				//con
				case 'a':
					stemp = getposition(pointer, upper);
					if (stemp % 10 == 1) { registers[pointer][0] += stemp/10; }
					else 
					{ registers[pointer][6]--; }
					break;
				case 's':
					stemp = getposition(pointer, upper);
					if (stemp % 10 == 1) { registers[pointer][0] -= stemp / 10; }
					else
					{ registers[pointer][6]--; }
					break;
				case 'm':
					stemp = getposition(pointer, upper);
					if (stemp % 10 == 1) { registers[pointer][0] *= stemp / 10; }
					else
					{ registers[pointer][6]--; }
					break;
				case 'd':
					stemp = getposition(pointer, upper);
					if (stemp % 10 == 1) { registers[pointer][0] /= stemp / 10; }
					else
					{ registers[pointer][6]--; }
					break;
				case 'r':
					stemp = getposition(pointer, upper);
					if (stemp % 10 == 1) { registers[pointer][0] %= stemp / 10; }
					else
					{ registers[pointer][6]--; }
					break;
				case 'c':
					stemp = getposition(pointer, upper);
					if (stemp % 10 == 1) { registers[pointer][0] = stemp / 10; }
					else
					{ registers[pointer][6]--; }
					break;
				//output
				case '^':
					Console.WriteLine(registers[pointer][0]);
					break;
				case '"':
					Console.WriteLine((char)registers[pointer][0]);
					break;
				//lifecycle
				case '=':
					active = false;
					break;
				case ':':
					lines.RemoveAt(pointer);
					registers.RemoveAt(pointer);
					array.RemoveAt(pointer);
					break;
				case 'y':
					//odio la mia vita
					string lineadd;
					if (lines[pointer].Contains('#'))
					{
						int index = lines[pointer].IndexOf('#')+1;
						lineadd = lines[pointer].Substring(index, lines[pointer].Length-index) ;
					}
					else { lineadd = lines[pointer]; }
					if (upper)
					{
						registers.Insert(pointer, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
						array.Insert(pointer, false);
						lines.Insert(pointer, lineadd);
						pointer++;
					}
					else
					{
						registers.Insert(pointer+1, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
						array.Insert(pointer+1, false);
						lines.Insert(pointer+1, lineadd);
						skip = 1;
					}
					break;
				case 'k':
					if (!upper)
					{
						if (pointer < lines.Count - 1)
						{

							registers.RemoveAt(pointer + 1);
							if (array[pointer + 1])
							{
								arrays.RemoveAt(int.Parse(lines[pointer + 1]));
							}
							array.RemoveAt(pointer + 1);
							lines.RemoveAt(pointer + 1);
						}
					}
					else
					{
						if (pointer > 0)
						{
							pointer--;
							registers.RemoveAt(pointer);
							if (array[pointer])
							{
								arrays.RemoveAt(int.Parse(lines[pointer]));
							}
							array.RemoveAt(pointer);
							lines.RemoveAt(pointer);
						}
					}
					break;

			}
			if (instruction != ':') 
			{
				registers[pointer][6]++;
				if (registers[pointer][6] == lines[pointer].Length || lines[pointer][registers[pointer][6]] == '#')
				{ registers[pointer][6] = 0; }
			}

		}
	}
	if (!array.Contains(false)) { active = false; }
	pointer++;
	if (pointer >= lines.Count) { pointer = 0; }
}