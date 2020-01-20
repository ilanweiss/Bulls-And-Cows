using System;
using System.Collections.Generic;

namespace BoolPgiaLogic
{
    public class GameLogic<T>
    {
        public const int k_NumOfGuesses = 4;
        private T[] m_ChosenSequence = new T[k_NumOfGuesses];
        private List<T> m_PossibleChoices;
        private Random m_Random;

        public GameLogic(List<T> i_PossibleChoices)
        {
            m_PossibleChoices = i_PossibleChoices;
            m_Random = new Random();
        }

        public int NumOfGuesses
        {
            get { return k_NumOfGuesses; }
        }

        public T[] ChosenSequence
        {
            get { return m_ChosenSequence; }
        }

        public void GenerateGame()
        {
            List<T> possibleChoices = new List<T>(m_PossibleChoices);

            for (int i = 0; i < m_ChosenSequence.Length; i++)
            {
                m_ChosenSequence[i] = possibleChoices[m_Random.Next(possibleChoices.Count)];
                possibleChoices.Remove(m_ChosenSequence[i]);
            }
        }

        public Guess CheckGuess(T[] i_UsersGuess)
        {
            int bulls = 0;
            int hits = 0;

            for (int i = 0; i < i_UsersGuess.Length; i++)
            {
                for (int j = 0; j < m_ChosenSequence.Length; j++)
                {
                    if (i_UsersGuess[i].Equals(m_ChosenSequence[j]))
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            hits++;
                        }
                    }
                }
            }
            return new Guess(bulls, hits);
        }
    }
}
