using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManeger
{

	// 单例模式：本类内部创建对象实例
	private static EquipmentManeger Instance = new EquipmentManeger();

    // 状态
    private bool isExhausted;

    // 电量
    private int currentElectricity;
	private int maxElectricity;
	private int electricityIncrease;
	private int electricityReduce;

    // 芯片卡槽
    private int currentChipNum;
    private int maxChipNum;

    public int CurrentElectricity { get => currentElectricity; set => currentElectricity = value; }
	public int MaxElectricity { get => maxElectricity; set => maxElectricity = value; }
    public bool IsExhausted { get => isExhausted; set => isExhausted = value; }
    public int CurrentChipNum { get => currentChipNum; set => currentChipNum = value; }
    public int MaxChipNum { get => maxChipNum; set => maxChipNum = value; }
    public int ElectricityReduce { get => electricityReduce; set => electricityReduce = value; }
    public int ElectricityIncrease { get => electricityIncrease; set => electricityIncrease = value; }


    // 构造器私有化，外部不能new
    private EquipmentManeger()
	{
		Initial();
	}

	// 提供一个公有的静态方法，返回实例对象
	public static EquipmentManeger getInstance()
	{
		return Instance;
	}

	private void Initial()
    {
		IsExhausted = false;

		maxElectricity = 100;
		currentElectricity = maxElectricity / 2;

		maxChipNum = 3;
		currentChipNum = 0;
    }

}