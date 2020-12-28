namespace RPG.Core
{
    public interface IAction {
        
        // buat interface agar dua class tidak saling berkegantungan
        // class mover dan Player combat akan connect ke Interface dan interface akan connect ke ActionScheduler
        // sehingga tidak ada class yang saling berkegantungan
         void cancelAction();
    }
}