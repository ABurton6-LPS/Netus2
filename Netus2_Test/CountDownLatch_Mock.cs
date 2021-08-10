using Netus2_DatabaseConnection.utilityTools;

namespace Netus2_Test
{
    public class CountDownLatch_Mock : CountDownLatch
    {
        public CountDownLatch_Mock(int count) : base(count) { }
        public void Reset(int count) 
        {
            //Do Nothing
        }
        public void Signal() 
        { 
            //Do Nothing
        }
        public void Wait() 
        {
            //Do Nothing
        }
    }
}
