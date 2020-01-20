namespace BoolPgiaLogic
{
    public struct Guess
    {
        private int m_Bulls;
        private int m_Hits;

        public Guess(int i_Bulls, int i_Hits)
        {
            m_Bulls = i_Bulls;
            m_Hits = i_Hits;
        }

        public int Bulls
        {
            get { return m_Bulls; }
        }

        public int Hits
        {
            get { return m_Hits; }
        }
    }
}
