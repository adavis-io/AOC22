using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    public class Day13 : Day
    {
        public class Message : IComparable
        {
            public List<object> messageData;
            public readonly bool isDividerMessage;

            public Message(string messageString, bool isDivider = false)
            {
                messageData = buildMessage(messageString);
                isDividerMessage = isDivider;
            }

            private List<object> buildMessage(string messageString)
            {
                Stack<List<object>> parseStack = new();

                List<object> current = new();
                List<object> parent = new();

                bool inInt = false;
                bool first = true;
                List<char> intStr = new();

                foreach(char c in messageString)
                {
                    if (c == '[')
                    {
                        if (!first)
                        {
                            parent = current;
                            parseStack.Push(current);
                            current = new();
                            parent.Add(current);
                        }
                    }
                    else if (c == ']')
                    {
                        if (inInt)
                        {
                            current.Add(int.Parse(new string(intStr.ToArray())));
                            intStr.Clear();
                        }
                        inInt = false;

                        if (parseStack.Count > 0)
                        {
                            current = parseStack.Pop();
                        }
                    }
                    else if (c == ',')
                    {
                        if(inInt)
                        {
                            current.Add(int.Parse(new string(intStr.ToArray())));
                            intStr.Clear();
                        }
                        inInt = false;


                    }
                    else
                    {
                        intStr.Add(c);
                        inInt = true;
                    }
                    first = false;
                }

                return current;
            }
            private int compareElementLists(List<object> left, List<object> right)
            {
                if (left.Count == 0)
                {
                    if (right.Count == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else if (right.Count == 0)
                {
                    return -1;
                }

                int recurseResult = 0;

                for(int i = 0; i < Math.Min(left.Count, right.Count); i++)
                {
                    recurseResult = 0;
                    if (left[i] is int)
                    {
                        if (right[i] is int)
                        {
                            //int-int comparison

                            if ((int)left[i] < (int)right[i])
                            {
                                return 1;
                            }
                            else if ((int)left[i] > (int)right[i])
                            {
                                return -1;
                            }
                        }
                        else
                        {
                            //int-list comparison. put int in list and handle as new list-list compare.

                            List<object> tempLeft = new();
                            
                            tempLeft.Add(left[i]);

                            recurseResult = compareElementLists(tempLeft, (List<object>)right[i]);
                        }
                    }
                    else
                    {
                        if (right[i] is int)
                        {
                            //list-int comparison. put int in list and handle as new list-list compare.
                            List<object> tempRight = new();
                            tempRight.Add(right[i]);

                            recurseResult = compareElementLists((List<object>)left[i], tempRight);
                        }
                        else
                        {
                            //list-list comparison. straight recursion.
                            recurseResult = compareElementLists((List<object>)left[i], (List<object>)right[i]);
                        }
                    }

                    if (recurseResult != 0)
                    {
                        return recurseResult;
                    }
                    
                    if (i == left.Count - 1) //end of left list
                    {
                        if (left.Count == right.Count) //end of both lists, no decision
                        {
                            return 0;
                        }

                        return 1;
                    }
                    else if (i == right.Count - 1) //end of right list
                    {
                        return -1;
                    }

                    else //more elements to consume
                    {
                        continue;
                    }

                }

                return 0;
            }
            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                Message otherMessage = obj as Message;
                if (otherMessage != null)
                {
                    return -1 * (compareElementLists(this.messageData, otherMessage.messageData));
                }
                else
                {
                    throw new ArgumentException("Object is not a Message");
                }
            }

            private string recursiveDataToString(List<object> Data)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for(int i = 0; i < Data.Count; i++)
                {
                    if(Data[i] is int)
                    {
                        sb.Append((int)Data[i]);
                    }
                    else if (Data[i] is List<object>) 
                    {
                        sb.Append(recursiveDataToString(Data[i] as List<object>));
                    }
                    if (i != Data.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("]");

                return sb.ToString();
            }

            public override string ToString()
            {
                return recursiveDataToString(this.messageData);   
            }
        }
        public Day13(bool test) : base(13, test)
        { 
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            List<Message> messages = new();
            
            foreach (var line in lines)
            {
                if(line.Length > 0)
                messages.Add(new Message(line));
            }

            int id = 0;
            int sum = 0;
            for(int i = 0; i < messages.Count; i += 2)
            {
                id += 1;
                if (messages[i].CompareTo(messages[i + 1]) < 0)
                {
                    sum += id;
                }
                if (messages[i].CompareTo(messages[i + 1]) == 0)
                {
                    Console.WriteLine("Comparison returned 0! Messages:");
                    Console.WriteLine("Left: " + messages[i].ToString());
                    Console.WriteLine("Right: " + messages[i + 1].ToString());
                }
               
            }
            Console.WriteLine(sum);

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();
            
            List<Message> messages = new();

            foreach (var line in lines)
            {
                if (line.Length > 0)
                    messages.Add(new Message(line));
            }

            messages.Add(new Message("[[2]]", true));
            messages.Add(new Message("[[6]]", true));


            messages.Sort();

            for (int i = 0; i < messages.Count - 1; i += 1)
            {
                if (messages[i].CompareTo(messages[i + 1]) > -1)
                {
                    Console.WriteLine("out of order message at index " + i);
                }
            }

            int id = 0;
            int decoder = 1;
            //Console.WriteLine("asdf");
            foreach (Message m in messages)
            {
                //Console.WriteLine(m.ToString());
                id += 1;
                if (m.isDividerMessage)
                {
                    decoder *= id;
                }

            }

            Console.WriteLine(decoder);

        }
    }
}
