using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfBot
{
    class BreadBuilder
    {
        #region Characters
        string[] A = { "OXO",
        "XOX",
        "XXX",
        "XOX",
        "XOX"};
        string[] B = { "XXO",
        "XOX",
        "XXO",
        "XOX",
        "XXO"};
        string[] C = { "XXX",
        "XOO",
        "XOO",
        "XOO",
        "XXX"};
        string[] D = { "XXO",
        "XOX",
        "XOX",
        "XOX",
        "XXO"};
        string[] E = { "XXX",
        "XOO",
        "XXX",
        "XOO",
        "XXX"};
        string[] F = { "XXX",
        "XOO",
        "XXO",
        "XOO",
        "XOO"};
        string[] G = { "OXX",
        "XOO",
        "XOX",
        "XOX",
        "OXO"};
        string[] H = { "XOX",
            "XOX",
            "XXX",
            "XOX",
            "XOX" };
        string[] I = { "XXX",
        "OXO",
        "OXO",
        "OXO",
        "XXX"};
        string[] J = { "OOX",
        "OOX",
        "OOX",
        "XOX",
        "OXO"};
        string[] K = { "XOX",
            "XOX",
            "XXO",
            "XOX",
            "XOX" };
        string[] L = { "XOO",
        "XOO",
        "XOO",
        "XOO",
        "XXX"};
        string[] M = { "XOX",
        "XXX",
        "XOX",
        "XOX",
        "XOX"};
        string[] N = { "XXO",
        "XOX",
        "XOX",
        "XOX",
        "XOX"};
        string[] O = { "XXX",
        "XOX",
        "XOX",
        "XOX",
        "XXX"};
        string[] P = { "XXX",
        "XOX",
        "XXX",
        "XOO",
        "XOO"};
        string[] Q = { "OXO",
        "XOX",
        "XOX",
        "OXO",
        "OOX"};
        string[] R = { "XXO",
        "XOX",
        "XXO",
        "XOX",
        "XOX"};
        string[] S = { "XXX",
        "XOO",
        "XXX",
        "OOX",
        "XXX"};
        string[] T = { "XXX",
        "OXO",
        "OXO",
        "OXO",
        "OXO"};
        string[] U = { "XOX",
        "XOX",
        "XOX",
        "XOX",
        "XXX"};
        string[] V = { "XOX",
        "XOX",
        "XOX",
        "OXO",
        "OXO"};
        string[] W = { "XOX",
        "XOX",
        "XOX",
        "XXX",
        "XOX"};
        string[] X = { "XOX",
        "XOX",
        "OXO",
        "XOX",
        "XOX"};
        string[] Y = { "XOX",
        "XOX",
        "OXO",
        "OXO",
        "OXO"};
        string[] Z = { "XXX",
        "OOX",
        "OXO",
        "XOO",
        "XXX"};
        string[] Space = {"O",
        "O",
        "O",
        "O",
        "O"};
        string[] Lenny = {
        "OXOOXXXOOOOXXXOXO",
        "XOOXOXOOXOXOXOOOX",
        "XOOOXOXOXOOXOXOOX",
        "XOOOOXOOOXOOXOOOX",
        "XOOOOOOOXOOOOOOOX",
        "XOOOOOXOOOXOOOOOX",
        "OXOOOOOXXXOOOOOXO"};
        string[] Colon = {
            "OOO",
            "OXO",
            "OOO",
            "OXO",
            "OOO"
        };
        string[] Caret = {
            "OXO",
            "XOX",
            "OOO",
            "OOO",
            "OOO"
        };
        string[] LBracket = {
            "OXO",
            "XOO",
            "XOO",
            "XOO",
            "OXO"
        };
        string[] RBracket = {
            "OXO",
            "OOX",
            "OOX",
            "OOX",
            "OXO"
        };
        #endregion

        string[] output = new string[7];

        void add(string[] letter, int a)
        {
            letter = convert(letter);
            for (int i = 0; i < letter.Length; i++)
            {
                output[i] += letter[i];
                if (a < 5 && letter != Caret && letter != Colon) output[i] += ":black_large_square:";
            }
        }

        string[] convert(string[] letter)
        {
            for (int i = 0; i < letter.Length; i++) letter[i] = letter[i].Replace("X", ":bread:").Replace("O", ":black_large_square:");
            return letter;
        }

        public string build(string sWord)
        {
            int num = 5;
            if (sWord != "( ͡° ͜ʖ ͡°)")
            { 
                char[] word = sWord.ToLower().ToCharArray();
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == 'a') add(A, i);
                    else if (word[i] == 'b') add(B, i);
                    else if (word[i] == 'c') add(C, i);
                    else if (word[i] == 'd') add(D, i);
                    else if (word[i] == 'e') add(E, i);
                    else if (word[i] == 'f') add(F, i);
                    else if (word[i] == 'g') add(G, i);
                    else if (word[i] == 'h') add(H, i);
                    else if (word[i] == 'i') add(I, i);
                    else if (word[i] == 'j') add(J, i);
                    else if (word[i] == 'k') add(K, i);
                    else if (word[i] == 'l') add(L, i);
                    else if (word[i] == 'm') add(M, i);
                    else if (word[i] == 'n') add(N, i);
                    else if (word[i] == 'o') add(O, i);
                    else if (word[i] == 'p') add(P, i);
                    else if (word[i] == 'q') add(Q, i);
                    else if (word[i] == 'r') add(R, i);
                    else if (word[i] == 's') add(S, i);
                    else if (word[i] == 't') add(T, i);
                    else if (word[i] == 'u') add(U, i);
                    else if (word[i] == 'v') add(V, i);
                    else if (word[i] == 'w') add(W, i);
                    else if (word[i] == 'x') add(X, i);
                    else if (word[i] == 'y') add(Y, i);
                    else if (word[i] == 'z') add(Z, i);
                    else if (word[i] == ' ') add(Space, i);
                    else if (word[i] == ':') add(Colon, i);
                    else if (word[i] == '^') add(Caret, i);
                    else if (word[i] == '(') add(LBracket, i);
                    else if (word[i] == ')') add(RBracket, i);
                }
            }
            else
            {
                add(Lenny, 5);
                num = 7;
            }

            string sOutput = "";
            for (int i = 0; i < num; i++)
            {
                sOutput += output[i] + "\n";
            }

            return sOutput;
        }
    }
}
