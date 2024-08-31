using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using System.Data;
public class Evaluator : MonoBehaviour
{
    private Cards cards;
    private Scope scope;

    void Start()
    {

    }
    public void eEffect()
    {
        foreach (var oAElement in cards.onActivation.Elements)
        {
            eOnActivationElements(oAElement);
        }
    }
    private void eOnActivationElements(OnActivationElements onActivationElements)
    {
        eOnActivationEffects(onActivationElements.oae, onActivationElements.selector);
        if (onActivationElements.postAction != null)
        {
            foreach (var postAction in onActivationElements.postAction)
            {
                ePostAction(postAction, onActivationElements.selector.Source);
            }
        }
    }

    private void eOnActivationEffects(OnActivationEffect onActivationEffect, Selector selector)
    {

        Effect effect = scope.effects[onActivationEffect.name];
        foreach (var assignment in onActivationEffect.Params)
        {
            scope.value[assignment.Left.name] = assignment.Right.Evaluate(scope);
        }
        if (selector != null)
        {
            scope.value["targets"] = eSelector(selector);
            eAction(effect.Action);
            scope.value.Remove("targets");
        }
        else
        {
            eAction(effect.Action);
        }
    }

    private void ePostAction(PostAction action, string selector)
    {
        Effect effect = scope.effects[(string)action.Type.Evaluate(scope)];
        foreach (var assignment in action.Assignments)
        {
            scope.value[assignment.Left.name] = assignment.Right.Evaluate(scope);
        }
        if (action.Selector != null)
        {
            scope.value["targets"] = eSelector(action.Selector, selector);
            eAction(effect.Action);
            scope.value.Remove("targets");
        }
        else
        {
            eAction(effect.Action);
        }
    }
    private void eAction(Action action)
    {
        eStamentBlock(action.Block);
    }
    private List<Cards> eSelector(Selector selector, string name = null)
    {
        List<Cards> cards = new List<Cards>();
        if (selector.Source == "parent") cards = eSource(name);
        else cards = eSource(selector.Source);
        List<Cards> cards2 = new List<Cards>();
        foreach (var cardss in cards)
        {
            Debug.Log("hola");
        }
        foreach (var card in cards)
        {
            scope.value[selector.Predicate.Variable.name] = card;
            if ((bool)selector.Predicate.Condition.Evaluate(scope)) cards2.Add(card);

            scope.value.Remove(selector.Predicate.Variable.name);
        }
        foreach (var cards1 in cards2)
        {
            Debug.Log("adios");
        }
        if (selector.Singles.Value)
        {
            List<Cards> cards3 = new List<Cards> { cards2[0] };
            return cards3;
        }
        else
        {
            return cards2;
        }
    }
    public void eStamentBlock(StatementBlock statementBlock)
    {
        foreach (var statement in statementBlock.statements)
        {
            statement.Evaluater(scope);
        }
    }
    private List<Cards> eSource(string name)
    {
        Debug.Log(name);
        switch (name)
        {
            case @"""hand""": return scope.gameManager.HandOfPlayer(scope.gameManager.TriggerPlayer());


            case @"""otherHand""":
                if (scope.gameManager.TriggerPlayer() == 1) return scope.gameManager.HandOfPlayer(0);
                else return scope.gameManager.HandOfPlayer(1);

            case @"""deck""": return scope.gameManager.DeckOfPlayer(scope.gameManager.TriggerPlayer());


            case @"""otherDeck""":
                if (scope.gameManager.TriggerPlayer() == 1) return scope.gameManager.DeckOfPlayer(0);
                else return scope.gameManager.DeckOfPlayer(1);

            case @"""field""": return scope.gameManager.FieldOfPlayer(scope.gameManager.TriggerPlayer());

            case @"""otherField""":
                if (scope.gameManager.TriggerPlayer() == 1) return scope.gameManager.FieldOfPlayer(0);
                else return scope.gameManager.FieldOfPlayer(1);
            case @"""board""":
                return scope.gameManager.Board();

            default: return scope.gameManager.Board();
        }
    }



    private Func<Cards, bool> ePredicate(Predicate predicateNode)
    {
        return card =>
            {
                scope.value[predicateNode.Variable.name] = card;
                return (bool)predicateNode.Condition.Evaluate(scope);
            };
    }

    public Evaluator(Scope scope, Cards cards)
    {
        this.scope = scope;
        this.cards = cards;
    }
}

