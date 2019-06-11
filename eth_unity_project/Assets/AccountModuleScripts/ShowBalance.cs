using UnityEngine;
using UnityEngine.UI;

public class ShowBalance : MonoBehaviour {
    public Text BalanceField;

	//void OnEnable()
	//{
	//    BlockChainController.DispatchUpdateBalanceEvent += UpdatedBalance;
	//}
	//void OnDisable()
	//{
	//    BlockChainController.DispatchUpdateBalanceEvent -= UpdatedBalance;
	//}
	//void UpdatedBalance(string currentBalance) {
	//    BalanceField.text = currentBalance;
	//}

    // Example that constantly checks for the latest balance. 
	void Update()
	{
        decimal x = GetComponent<BalanceController>().CurrentBalance;
        BalanceField.text = x.ToString();
	}
}
