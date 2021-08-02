using Netus2_DatabaseConnection.utilityTools;

namespace Netus2SisSync.UtilityTools
{
    public class CountDownLatch_Mock : CountDownLatch
    {
        public CountDownLatch_Mock(int count) : base(count) { }
        public void Reset(int count) { }
        public void Signal() { }
        public void Wait() { }
    }
}
