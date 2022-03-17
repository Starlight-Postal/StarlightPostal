using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariableManager {

    private static GameVariableManager inst;

    public static Instance() {
        if (inst == null) {
            inst = new GameVariableManager();
        }

        return inst;
    }

    private IDictionary<string, GameVar> vars = new Dictionary();

    public GameVariableManager() {
        
    }

    public GameVar GetVar(string name) {
        return vars.TryGetValue(name);
    }

    public void SetNewVar(string name, GameVar var) {
        vars.Add(name, var);
    }

    public void GetOrCreate(string name, GameVar fallback) {
        if (!vars.ContainsKey(name)) {
            SetNewVar(name, fallback);
        }
        
        return GetVar(name);
    }

}

public class GameVar<T> {
    
    private T data;

    public GameVar(T initData) {
        data = initData;
    }

    public T read() {
        return data;
    }

    public void write(T newData) {
        data = newData;
    }

}
