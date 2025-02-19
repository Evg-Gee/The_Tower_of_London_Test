using Data;

namespace Scripts
{
    public interface IPole
    {
        bool CanAccept(Ring ring);
        void AddRing(Ring ring); 
        void RemoveRing(Ring ring); 
        bool IsInTargetState();
        Ring GetTopRing();
        void Clear(); 
        void SetTargetState(PoleTargetState targetState); 
    }
}