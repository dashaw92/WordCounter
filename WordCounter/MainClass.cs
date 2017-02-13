using System;
using System.Windows.Input;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace WordCounter
{
	class MainClass
	{
		private static string current_word = "";
		private static LowLevelKeyboardListener listener;
		private static Key last_key;
		private static Dictionary<string, int> subscribed;
		private static Form form;

		static void Main()
		{
			subscribed = new Dictionary<string, int>();
			subscribed.Add("like", 0);
			listener = new LowLevelKeyboardListener();
			listener.OnKeyPressed += OnKeyPress;
			listener.HookKeyboard();
			form = new Form();
			form.Width = 0;
			form.Height = 0;
			form.ShowInTaskbar = false;
			form.Shown += Form_Shown;
			form.Activated += Form_Activated;
			form.ShowDialog();
		}

		private static void Form_Activated(object sender, EventArgs e)
		{
			form.SetDesktopLocation(-100, -100);
		}

		private static void Form_Shown(object sender, EventArgs e)
		{
			form.SetDesktopLocation(-100, -100);
		}

		private static void compile_stats()
		{
			string output = string.Format("{0,6} {1,15}\n", "Word", "Count");
			foreach(string item in subscribed.Keys)
			{
				output += string.Format("{0,6} {1,15}\n", item, subscribed[item]);
			}
			MessageBox.Show(output, "Word counter statistics", MessageBoxButtons.OK);
		}

		private static void OnKeyPress(object sender, KeyPressedArgs e)
		{
			switch (e.KeyPressed.ToString().Length)
			{
				case 1:
					//All options in the Key enum that are one character long are letters.
					current_word += e.KeyPressed.ToString().ToLower();
					break;
				case 2:
					//F1 -> F9
					goto case 3;
				case 3:
					//F10 -> ...
					switch(last_key)
					{
						case Key.F2:
							if(e.KeyPressed == Key.F3)
							{
								//F2 + F3 pressed (stats)
								compile_stats();
								last_key = Key.Enter;
							}
							else if(e.KeyPressed == Key.F4)
							{
								//F2 + F4 pressed (exits)
								compile_stats();
								Application.Exit();
							} else if(e.KeyPressed == Key.F5)
							{
								//F2 + F5 pressed (add subscription)
								string word = Microsoft.VisualBasic.Interaction.InputBox("Add word subscription", "Enter a word to subscribe to", "", 0, 0).Trim().ToLower();
								if (!subscribed.ContainsKey(word))
								{
									subscribed.Add(word, 0);
								}
							}
							break;
						case Key.F3:
							if (e.KeyPressed == Key.F2)
							{
								//F2 + F3 pressed (stats)
								compile_stats();
								last_key = Key.Enter;
							}
							break;
						case Key.F4:
							if (e.KeyPressed == Key.F2)
							{
								//F2 + F4 pressed (exits)
								compile_stats();
								listener.UnHookKeyboard();
								Application.Exit();
							}
							break;
						case Key.F5:
							if(e.KeyPressed == Key.F2)
							{
								//F1 + F4 pressed (add subscription)
								string word = Microsoft.VisualBasic.Interaction.InputBox("Add word subscription", "Enter a word to subscribe to", "", 0, 0).Trim().ToLower();
								if (!subscribed.ContainsKey(word))
								{
									subscribed.Add(word, 0);
								}
							}
							break;
					}
					last_key = e.KeyPressed;
					break;
				case 5:
					goto case 6;
				case 6:
					//Space or Enter
					if(e.KeyPressed == Key.Space || e.KeyPressed == Key.Enter)
					{
						foreach(string item in subscribed.Keys)
						{
							if (current_word.Equals(item))
							{
								subscribed[item] += 1;
								break;
							}
						}
						Console.WriteLine("You typed the word \"" + current_word + "\"");
						current_word = "";
					}
					break;
				default:
					last_key = Key.Enter;
					break;
			}
		}
	}
}