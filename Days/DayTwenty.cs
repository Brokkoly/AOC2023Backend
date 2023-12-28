namespace AOC2023Backend.Days
{
    public class DayTwenty : Day
    {
        public override string PartOne(string input)
        {
            var modules = ParseInput(input).Select(x => ModuleFactory.CreateModule(x)).ToList();
            var buttonModule = ModuleFactory.CreateButtonModule();
            modules.Add(buttonModule);
            foreach (var module in modules)
            {
                module.InitializeModules(modules);
            }
            Module.InitializeInputModules(modules);
            foreach (var module in modules)
            {
                module.FinishUp();
            }
            var broadcastModule = modules.Find(x => x.Name == "broadcaster") ?? throw new Exception();

            long highPulses = 0;
            long lowPulses = 0;
            var i = 0;
            var rxHit = false;
            for (i = 0; i < 1000; i++)
            {

                var pulseQueue = new Queue<Pulse>();
                pulseQueue.Enqueue(new Pulse(buttonModule, broadcastModule, false));
                var pulsesDone = new List<Pulse>();
                while (pulseQueue.Count > 0)
                {
                    var pulse = pulseQueue.Dequeue();
                    pulsesDone.Add(pulse);
                    if (pulse.IsHigh)
                    {
                        highPulses++;
                    }
                    else
                    {
                        lowPulses++;
                    }
                    var result = pulse.Destination.HandlePulse(pulse);
                    result.ForEach(x => pulseQueue.Enqueue(x));
                }
            }

            return (highPulses * lowPulses).ToString();
        }

        public override string PartTwo(string input)
        {
            var modules = ParseInput(input).Select(x => ModuleFactory.CreateModule(x)).ToList();
            var buttonModule = ModuleFactory.CreateButtonModule();
            modules.Add(buttonModule);
            foreach (var module in modules)
            {
                module.InitializeModules(modules);
            }
            Module.InitializeInputModules(modules);
            foreach (var module in modules)
            {
                module.FinishUp();
            }
            var broadcastModule = modules.Find(x => x.Name == "broadcaster") ?? throw new Exception();

            var i = 1;
            var rxHit = false;

            Dictionary<string, List<bool>> moduleStates = new();
            Dictionary<string, List<bool>> conjunctionStates = new();
            Dictionary<string, List<int>> conjugationStateHighs = new();
            var dd = (ConjunctionModule)(modules.Find(x => x.Name == "dd") ?? throw new Exception());
            foreach (var module in modules)
            {
                if (module.MyModuleType == Module.ModuleType.Conjunction)
                {
                    //conjunctionStates.Add(module.Name, new(1000));
                    conjugationStateHighs.Add(module.Name, new List<int>());
                }
            }
            for (i = 0; i <= 20000; i++)
            {
                var pulseQueue = new Queue<Pulse>();
                pulseQueue.Enqueue(new Pulse(buttonModule, broadcastModule, false));
                var pulsesDone = new List<Pulse>();
                while (pulseQueue.Count > 0)
                {
                    var pulse = pulseQueue.Dequeue();
                    if (pulse.Destination.Name == "dd" && pulse.IsHigh)
                    {
                        conjugationStateHighs[pulse.Origin.Name].Add(i);
                        //if (conjugationStateHighs[pulse.Origin.Name] == -1)
                        //{
                        //    conjugationStateHighs[pulse.Origin.Name] = i;
                        //}
                    }
                    if (pulse.Destination.Name == "rx" && pulse.IsLow)
                    {
                        rxHit = true;
                        return i.ToString();
                    }
                    pulsesDone.Add(pulse);
                    var result = pulse.Destination.HandlePulse(pulse);
                    result.ForEach(x => pulseQueue.Enqueue(x));
                }
                foreach (var module in modules)
                {
                    if (module.MyModuleType == Module.ModuleType.Conjunction)
                    {
                        var low = ((ConjunctionModule)module).ShouldBeLow();
                        //conjunctionStates[module.Name].Add(low);
                        //if (!low)
                        //{
                        //    if (conjugationStateHighs[module.Name] == -1)
                        //    {
                        //        conjugationStateHighs[module.Name] = i;
                        //    }
                        //}
                    }
                }
                //foreach (var keyValuePair in ((ConjunctionModule)(modules.Find(x => x.Name == "dd") ?? throw new Exception())).PreviousPulses)
                //{
                //    moduleStates[keyValuePair.Key].Add(keyValuePair.Value);
                //}
            }
            //var anyNotSame = conjunctionStates.D().ToList();

            var loopNums = conjugationStateHighs.Values.Where(x => x.Count > 0).Select(x => x[1] - x[0]).OrderByDescending(x => x).ToList();
            var primes = FindAllPrimes(loopNums[0]);
            //var primeFactors = loopNums.Select(x => FindPrimeFactors(x, ref primes)).ToList();
            long multNum = 1;
            //foreach (var factor in primeFactors)
            //{
            //    foreach (var factor2 in factor)
            //    {
            //        multNum *= factor2;
            //    }

            //}
            foreach (var num in loopNums)
            {
                multNum *= num;
            }
            return multNum.ToString();
        }
        public List<int> FindAllPrimes(int upTo)
        {
            var maxNum = upTo / 2;
            var allPrimes = new List<int> { 2 };

            for (int i = 3; i <= maxNum; i += 2)
            {
                var isPrime = true;
                for (int j = 0; j < allPrimes.Count; j++)
                {
                    if (i % allPrimes[j] == 0)
                    {
                        isPrime = false; break;
                    }
                }
                if (isPrime)
                {
                    allPrimes.Add(i);
                }
            }
            return allPrimes;
        }
        public List<int> FindPrimeFactors(int input, ref List<int> primes)
        {
            var retPrimes = new List<int>();
            var numToUse = input;
            if (input % 2 == 0)
            {
                do
                {
                    retPrimes.Add(2);
                    numToUse = numToUse / 2;
                } while (numToUse % 2 == 0);
            }

            for (var i = 0; i <= primes.Count && primes[i] <= numToUse && numToUse > 1;)
            {
                if (numToUse % primes[i] == 0)
                {
                    retPrimes.Add(primes[i]);
                    numToUse = numToUse / primes[i];
                    continue;
                }
                i++;
            }
            return retPrimes;
        }

        public int FindPatterNumber(List<bool> input)
        {
            var size = input.Count / 4;
            while (true)
            {
                var inPattern = true;
                for (var x = 0; x < size; x++)
                {
                    var firstArrNode = input[input.Count - 1 - x];
                    var secondArrNode = input[input.Count - 1 - x - size];
                    var thirdArrNode = input[input.Count - 1 - x - size * 2];
                    if (firstArrNode != secondArrNode || firstArrNode != thirdArrNode)
                    {
                        inPattern = false;
                        break;
                    }
                }
                if (inPattern)
                {
                    return size;
                }
                size--;
            }

            return -1;
        }


    }

    public static class ModuleFactory
    {
        public static Module CreateModule(string input)
        {
            var leftAndRight = input.Split(" -> ");
            var destinations = leftAndRight[1].Split(", ").ToList();

            switch (leftAndRight[0][0])
            {
                case 'b':
                    return new BroadcastModule(leftAndRight[0], destinations);
                case '%':
                    return new FlipFlopModule(leftAndRight[0][1..], destinations);
                case '&':
                    return new ConjunctionModule(leftAndRight[0][1..], destinations);
                default:
                    throw new Exception();
            }
        }
        public static ButtonModule CreateButtonModule()
        {
            return new ButtonModule("button", new() { "broadcaster" });
        }
    }

    public class BroadcastModule : Module
    {
        public BroadcastModule(string name, List<string> destinationNames) : base(name, destinationNames)
        {
            MyModuleType = ModuleType.Broadcast;
        }

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            var pulses = new List<Pulse>();
            foreach (var module in DestinationModules)
            {
                pulses.Add(new Pulse(this, module, pulse.IsHigh));
            }
            return pulses;
        }
    }

    public class DudModule : Module
    {
        public DudModule(string name, List<string> destinationNames) : base(name, destinationNames)
        {
            MyModuleType = ModuleType.Dud;
        }

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            return new();
        }
    }

    public class FlipFlopModule : Module
    {

        public bool On { get; set; } = false;
        public FlipFlopModule(string name, List<string> destinationNames) : base(name, destinationNames)
        {
            MyModuleType = ModuleType.FlipFlop;
        }

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            var pulses = new List<Pulse>();
            if (pulse.IsHigh)
            {
                return pulses;
            }
            else
            {
                On = !On;

                foreach (var module in DestinationModules)
                {
                    pulses.Add(new Pulse(this, module, On));
                }
                return pulses;
            }
        }
    }

    public class ConjunctionModule : Module
    {
        public Dictionary<string, bool> PreviousPulses = new Dictionary<string, bool>();
        public ConjunctionModule(string name, List<string> destinationNames) : base(name, destinationNames)
        {
            MyModuleType = ModuleType.Conjunction;
        }

        public override void FinishUp()
        {
            base.FinishUp();
            foreach (var module in InputModules)
            {
                PreviousPulses.Add(module.Name, false);
            }
        }

        public bool ShouldBeLow()
        {
            var sendLow = true;
            foreach (var value in PreviousPulses.Values)
            {
                if (!value)
                {
                    sendLow = false;
                    break;
                }
            }
            return sendLow;
        }

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            PreviousPulses[pulse.Origin.Name] = pulse.IsHigh;
            var sendLow = ShouldBeLow();

            var pulses = new List<Pulse>();
            foreach (var module in DestinationModules)
            {
                pulses.Add(new Pulse(this, module, !sendLow));
            }
            return pulses;
        }

    }



    public abstract class Module
    {
        public enum ModuleType
        {
            Conjunction,
            FlipFlop,
            Abstract,
            Dud,
            Broadcast,
        }
        public ModuleType MyModuleType { get; protected set; } = ModuleType.Abstract;
        public string Name { get; set; }

        public List<Module> InputModules { get; set; } = new List<Module>();

        public List<Module> DestinationModules { get; set; } = new List<Module>();

        public List<string> DestinationNames { get; set; } = new List<string>();

        //public Queue<Pulse> IncomingPulses { get; set; } = new Queue<Pulse>();
        protected Module(string name, List<string> destinationNames) { Name = name; DestinationNames.AddRange(destinationNames); }

        public void InitializeModules(List<Module> modules)
        {
            DestinationModules.AddRange(modules.Where(module => DestinationNames.Contains(module.Name)));
        }

        public static void InitializeInputModules(List<Module> modules)
        {
            var InputMap = new Dictionary<string, List<Module>>();

            foreach (var module in modules)
            {
                foreach (var destinationModule in module.DestinationModules)
                {
                    if (!InputMap.ContainsKey(destinationModule.Name))
                    {
                        InputMap.Add(destinationModule.Name, new List<Module>());
                    }
                    InputMap[destinationModule.Name].Add(module);
                }
            }

            foreach (var module in modules)
            {
                if (InputMap.ContainsKey(module.Name))
                {
                    module.InputModules.AddRange(InputMap[module.Name]);
                }
            }
        }

        public virtual void FinishUp()
        {
            if (DestinationModules.Count != DestinationNames.Count)
            {
                foreach (var name in DestinationNames)
                {
                    if (!DestinationModules.Any(x => x.Name == name))
                    {
                        DestinationModules.Add(new DudModule(name, new()));
                    }
                }
            }
        }

        public abstract List<Pulse> HandlePulse(Pulse pulse);
    }

    public class ButtonModule : Module
    {
        public ButtonModule(string name, List<string> destinationNames) : base(name, destinationNames)
        {
        }

        public override List<Pulse> HandlePulse(Pulse pulse)
        {
            return new() { new Pulse(this, DestinationModules[0], false) };
        }
    }

    public class Pulse
    {
        public Module Origin { get; set; }
        public bool IsHigh { get; private set; }
        public bool IsLow { get => !IsHigh; }

        public Module Destination { get; set; }

        //public void SetPulseState(bool isHigh)
        //{
        //    IsHigh = isHigh;
        //}
        public Pulse(Module origin, Module destination, bool isHigh)
        {
            Origin = origin;
            Destination = destination;
            IsHigh = isHigh;
        }

    }
}
