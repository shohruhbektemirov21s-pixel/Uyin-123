using System;
using System.Collections.Generic;
namespace BlockBlast.Utils {
    public class ObjectPool<T> where T : class {
        private readonly Stack<T> _pool;
        private readonly Func<T> _create;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;
        public ObjectPool(int capacity, Func<T> create, Action<T> onGet = null, Action<T> onRelease = null) {
            _pool = new Stack<T>(capacity); _create = create; _onGet = onGet; _onRelease = onRelease;
            for (int i = 0; i < capacity; i++) _pool.Push(_create());
        }
        public T Get() {
            T obj = _pool.Count > 0 ? _pool.Pop() : _create();
            _onGet?.Invoke(obj);
            return obj;
        }
        public void Release(T obj) { _onRelease?.Invoke(obj); _pool.Push(obj); }
    }
}
