﻿//
// ResourcePolicy.cs
//
// Author(s):
//       Alessio Parma <alessio.parma@gmail.com>
//
// Copyright (c) 2012-2014 Alessio Parma <alessio.parma@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace Dessert.Examples.CSharp
{
    using System;
    using System.Collections.Generic;
    using Resources;

    public static class ResourcePolicy
    {
        static IEnumerable<SimEvent> Getter(Store<int> store, char id)
        {
            var getEv = store.Get();
            yield return getEv;
            Console.WriteLine("{0}: {1}", id, getEv.Value);
        }

        public static void Run()
        {
            var env = Sim.NewEnvironment(seed: 21);
            const int capacity = 10;
            var store = Sim.NewStore<int>(env, capacity, WaitPolicy.FIFO, WaitPolicy.FIFO, WaitPolicy.Random);
            for (var i = 0; i < capacity; ++i) {
                store.Put(i);
            }
            for (var i = 0; i < capacity; ++i) {
                env.Process(Getter(store, (char) ('A' + i)));
            }
            env.Run();
        }
    }
}