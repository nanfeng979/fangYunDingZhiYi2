public interface ChessObject_NetWork_Interface
{
    void AddEquipmentColumn_Network(string equipmentColumnIndex_str, string equipmentName);
    void NormalAttackAnimation_Network(string beAttackedChessObjectID_str, string attackSpeed_str);
}

public interface Prop_NetWork_Interface
{
    void UseProp_Network();
}