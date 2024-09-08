using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter {
    [SerializeField] private KitchenRecipeSO[] cuttingRecipes;

    public event EventHandler OnplayerCutObject;



    public void Cut(PlayerScript player) {
        OnplayerCutObject?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(PlayerScript player) {
        if (!HasKitchenObject()) {
            //there is no object in the counter
            if (player.HasKitchenObject()) {
                //player has an object
                if (ObjectIsCuttable(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectHolder(this);
                }
            }
            else {
                //player has no object
            }
        }
        else {
            //there is an object in the counter
            if (player.HasKitchenObject()) {
                //player has an object
            }
            else {
                //player has no object
                this.GetKitchenObject().SetKitchenObjectHolder(player);
            }
        }

    }
    public override void InteractAlternate(PlayerScript player) {
        if (HasKitchenObject() && ObjectIsCuttable(GetKitchenObject().GetKitchenObjectSO())) {
            KitchenObject_SO input = GetKitchenObject().GetKitchenObjectSO();
            KitchenObject_SO output = GetOutputFromInput(input);
                GetKitchenObject().Destroy();
                KitchenObject.CreateKitchenObject(output, this);
                Cut(player);            
        }

    }

    public bool ObjectIsCuttable(KitchenObject_SO inputObject) {
        foreach (KitchenRecipeSO recipe in cuttingRecipes) {
            if (recipe.input == inputObject) {
                return true;
            }
        }
        return false;
    }

    public KitchenObject_SO GetOutputFromInput(KitchenObject_SO input) {
        foreach (KitchenRecipeSO recipe in cuttingRecipes) {
            if (recipe.input == input) {
                return recipe.output;
            }
        }
        return null;
    }
}