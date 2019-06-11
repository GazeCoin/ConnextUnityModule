using UnityEngine;
using UnityEngine.UI;

public class ShowBalance : MonoBehaviour {
    public Text BalanceField;
    void OnEnable()
    {
        BlockChainController.DispatchUpdateBalanceEvent += UpdatedBalance;
    }
    void OnDisable()
    {
        BlockChainController.DispatchUpdateBalanceEvent -= UpdatedBalance;
    }
    void UpdatedBalance(string currentBalance) {
        BalanceField.text = currentBalance;
    }
}
