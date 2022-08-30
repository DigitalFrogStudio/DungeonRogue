﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.DungeonRogue.Scripts
{
    public class Inventory
    {
        public event Action<int, Cell, Cell> OnInventoryChanged;

        private List<Cell> inventoryCells;

        public readonly int cellsAmount;

        public Inventory(int cellsAmount)
        {
            this.cellsAmount = cellsAmount;
            inventoryCells = new List<Cell>(cellsAmount);

            for (int i = 0; i < cellsAmount; i++)
            {
                inventoryCells.Add(new Cell());
            }
        }

        public bool AttemptAdd(ItemData itemData)
        {
            // Индекс первой пустой ячейки в инвентаре
            int firstEmptyCellIndex = -1;

            for (int i = 0; i < cellsAmount; i++)
            {
                if (inventoryCells[i].IsEmpty == false)
                {
                    // Если название предмета в текущей ячейке совпадает с названием добавляемого предмета
                    // И
                    // Если количество предметов в текущей ячейке меньше максимального
                    if ((inventoryCells[i].StoredItem.ID == itemData.ID) &&
                        (inventoryCells[i].Amount < itemData.LimitPerInventoryCell))
                    {
                        Cell cellBefore = inventoryCells[i];
                        
                        Cell cellAfter = cellBefore;
                        cellAfter.Amount += 1;

                        inventoryCells[i] = cellAfter;

                        OnInventoryChanged?.Invoke(i, cellBefore, cellAfter);

                        return true;
                    }
                }
                // Иначе, получается, что ячейка пустая, и, если мы ни разу не находили пустую ячеку, записываем индекс текущей
                else if (firstEmptyCellIndex == -1)
                {
                    firstEmptyCellIndex = i;
                }
            }
            // Дальнейший код выполняется, если не нашли незаполненную ячейку с таким же именем

            // Если находили пустую ячейку
            if (firstEmptyCellIndex != -1)
            {
                Cell cellBefore = inventoryCells[firstEmptyCellIndex];

                Cell cellAfter = cellBefore;
                cellAfter.StoredItem = itemData;
                cellAfter.Amount = 1;

                // Добавляем предмет в неё
                inventoryCells[firstEmptyCellIndex] = cellAfter;

                OnInventoryChanged?.Invoke(firstEmptyCellIndex, cellBefore, cellAfter);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveOne(int index, out ItemData itemData)
        {
            itemData = new ItemData();

            if (inventoryCells[index].IsEmpty == true)
            {
                return false;
            }
            else
            {
                Cell cellBefore = inventoryCells[index];

                Cell cellAfter = cellBefore;
                cellAfter.Amount--;

                inventoryCells[index] = cellAfter;

                itemData = inventoryCells[index].StoredItem;

                OnInventoryChanged?.Invoke(index, cellBefore, cellAfter);

                return true;
            }
        }

        public bool RemoveOne(int id)
        {
            int indexToRemove = inventoryCells.FindIndex(x => x.StoredItem.ID == id);
            if (indexToRemove == -1)
            {
                return false;
            }

            Cell cellBefore = inventoryCells[indexToRemove];

            Cell cellAfter = cellBefore;
            cellAfter.Amount--;

            inventoryCells[indexToRemove] = cellAfter;

            OnInventoryChanged?.Invoke(indexToRemove, cellBefore, cellAfter);

            return true;
        }

        public int Remove(int index, out ItemData itemData)
        {
            itemData = new ItemData();

            if (inventoryCells[index].IsEmpty == true)
            {
                return 0;
            }
            else
            {
                Cell cellBefore = inventoryCells[index];

                Cell cellAfter = new Cell();

                inventoryCells[index] = cellAfter;

                itemData = inventoryCells[index].StoredItem;

                OnInventoryChanged?.Invoke(index, cellBefore, cellAfter);

                return cellBefore.Amount;
            }
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            Cell tmpCell = inventoryCells[firstIndex];

            inventoryCells[firstIndex] = inventoryCells[secondIndex];
            OnInventoryChanged?.Invoke(firstIndex, inventoryCells[secondIndex], inventoryCells[firstIndex]);

            inventoryCells[secondIndex] = tmpCell;
            OnInventoryChanged?.Invoke(secondIndex, inventoryCells[firstIndex], inventoryCells[secondIndex]);
        }

        public void LogInventory()
        {
            StringBuilder inventoryStringBuilder = new StringBuilder("Inventory contains:");
            
            for (int i = 0; i < cellsAmount; i++)
            {
                inventoryStringBuilder.Append("\nCell №" + i + ": ");

                if (inventoryCells[i].IsEmpty)
                {
                    inventoryStringBuilder.Append("Empty");
                }
                else
                {
                    inventoryStringBuilder.Append(inventoryCells[i].Amount.ToString() + " thing(s) of id = " + inventoryCells[i].StoredItem.ID);
                }
            }

            Debug.Log(inventoryStringBuilder.ToString());
        }

        private bool ValidateIndex(int index)
        {
            if ((index > 0) && (index < inventoryCells.Count))
            {
                return true;
            }
            else
            {
                Debug.LogWarning("Attempt to address an index out of the inventory list range!");

                return false;
            }
        }
    }
}