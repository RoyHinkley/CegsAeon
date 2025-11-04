using System;
using AeonHacs;
namespace AeonHacs.Components;
using AeonHacs.Utilities;
using System.Threading.Tasks;
using static AeonHacs.Utilities.Utility;

public partial class CegsAeon : Cegs
{
    #region HacsComponent
    #endregion HacsComponent

    #region System configuration
    #region HacsComponents
    #endregion HacsComponents
    #endregion System configuration

    #region Process Management

    protected override void BuildProcessDictionary()
    {
        Separators.Clear();

        // Running samples
        ProcessDictionary["Run samples"] = RunSamples;
        Separators.Add(ProcessDictionary.Count);

        // Preparation for running samples
        ProcessDictionary["Prepare GRs for new iron and desiccant"] = PrepareGRsForService;
        ProcessDictionary["Precondition GR iron"] = PreconditionGRs;
        ProcessDictionary["Replace iron in sulfur traps"] = ChangeSulfurFe;
        ProcessDictionary["Prepare loaded inlet ports for collection"] = PrepareInletPorts;
        Separators.Add(ProcessDictionary.Count);

        ProcessDictionary["Prepare carbonate sample for acid"] = PrepareCarbonateSample;
        ProcessDictionary["Load acidified carbonate sample"] = LoadCarbonateSample;
        Separators.Add(ProcessDictionary.Count);

        // Open line
        ProcessDictionary["Open and evacuate line"] = OpenLine;
        Separators.Add(ProcessDictionary.Count);

        // Main process continuations
        ProcessDictionary["Collect, etc."] = CollectEtc;
        ProcessDictionary["Extract, etc."] = ExtractEtc;
        ProcessDictionary["Measure, etc."] = MeasureEtc;
        ProcessDictionary["Graphitize, etc."] = GraphitizeEtc;
        Separators.Add(ProcessDictionary.Count);

        // Top-level steps for standard protocol
        ProcessDictionary["Admit sealed CO2 to InletPort"] = AdmitSealedCO2IP;
        ProcessDictionary["Collect CO2 from InletPort"] = Collect;
        ProcessDictionary["Extract"] = Extract;
        ProcessDictionary["Measure"] = Measure;
        ProcessDictionary["Discard excess CO2 by splits"] = DiscardSplit;
        ProcessDictionary["Remove sulfur"] = RemoveSulfur;
        ProcessDictionary["Dilute small sample"] = Dilute;
        ProcessDictionary["Graphitize aliquots"] = GraphitizeAliquots;
        Separators.Add(ProcessDictionary.Count);

        // Secondary-level process sub-steps
        ProcessDictionary["Evacuate Inlet Port"] = EvacuateIP;
        ProcessDictionary["Flush Inlet Port"] = FlushIP;
        ProcessDictionary["Admit O2 into Inlet Port"] = AdmitIPO2;
        ProcessDictionary["Heat Quartz and Open Line"] = HeatQuartzOpenLine;
        ProcessDictionary["Turn off IP furnaces"] = TurnOffIPFurnaces;
        ProcessDictionary["Discard IP gases"] = DiscardIPGases;
        ProcessDictionary["Close IP"] = CloseIP;
        ProcessDictionary["Prepare for collection"] = PrepareForCollection;
        ProcessDictionary["Start collecting"] = StartCollecting;
        ProcessDictionary["Clear collection conditions"] = ClearCollectionConditions;
        ProcessDictionary["Collect until condition met"] = CollectUntilConditionMet;
        ProcessDictionary["Stop collecting"] = StopCollecting;
        ProcessDictionary["Stop collecting immediately"] = StopCollectingImmediately;
        ProcessDictionary["Stop collecting after bleed down"] = StopCollectingAfterBleedDown;
        ProcessDictionary["Evacuate and Freeze first trap"] = FreezeFirstTrap;
        ProcessDictionary["Evacuate and Freeze VTT"] = FreezeVtt;
        ProcessDictionary["Admit Dead CO2 into MC"] = AdmitDeadCO2;
        ProcessDictionary["Purify CO2 in MC"] = CleanupCO2InMC;
        ProcessDictionary["Discard MC gases"] = DiscardMCGases;
        ProcessDictionary["Divide sample into aliquots"] = DivideAliquots;
        Separators.Add(ProcessDictionary.Count);

        // Split sample processing
        ProcessDictionary["Prepare for split sample processing"] = InitializeSplits;
        ProcessDictionary["Collect a sample split; send it to the VTT"] = CollectSampleSplit;
        ProcessDictionary["Graphitize all collected splits"] = GraphitizeSplits;
        ProcessDictionary["Clear split sample processing data"] = ClearSplits;
        Separators.Add(ProcessDictionary.Count);

        // Granular inlet port & sample process control
        ProcessDictionary["Reset Inlet Port to Loaded"] = ResetIpToLoaded;
        ProcessDictionary["Freeze the Inlet Port"] = FreezeIp;
        ProcessDictionary["Raise LN on the Inlet Port"] = RaiseLNIp;
        ProcessDictionary["Thaw the Inlet Port"] = ThawIp;
        ProcessDictionary["Raise IP furnaces"] = RaiseIpFurnaces;
        ProcessDictionary["Turn on quartz furnace"] = TurnOnIpQuartzFurnace;
        ProcessDictionary["Turn off quartz furnace"] = TurnOffIpQuartzFurnace;
        ProcessDictionary["Disable sample setpoint ramping"] = DisableIpRamp;
        ProcessDictionary["Enable sample setpoint ramping"] = EnableIpRamp;
        ProcessDictionary["Turn on sample furnace"] = TurnOnIpSampleFurnace;
        ProcessDictionary["Adjust sample setpoint"] = AdjustIpSetpoint;
        ProcessDictionary["Adjust sample ramp rate"] = AdjustIpRampRate;
        ProcessDictionary["Wait for sample to rise to setpoint"] = WaitIpRiseToSetpoint;
        ProcessDictionary["Wait for sample to fall to setpoint"] = WaitIpFallToSetpoint;
        ProcessDictionary["Turn off sample furnace"] = TurnOffIpSampleFurnace;
        Separators.Add(ProcessDictionary.Count);

        // General-purpose process control actions
        ProcessDictionary["Wait for timer"] = WaitForTimer;
        ProcessDictionary["Wait for IP timer"] = WaitIpMinutes;
        ProcessDictionary["Wait for operator"] = Notify.WaitForOperator;
        Separators.Add(ProcessDictionary.Count);

        // Transferring CO2
        ProcessDictionary["Transfer CO2 from CT to VTT"] = TransferCO2FromCTToVTT;
        ProcessDictionary["Transfer CO2 from MC to VTT"] = TransferCO2FromMCToVTT;
        ProcessDictionary["Transfer CO2 from MC to GR"] = TransferCO2FromMCToGR;
        ProcessDictionary["Transfer CO2 from prior GR to MC"] = TransferCO2FromGRToMC;
        Separators.Add(ProcessDictionary.Count);

        // Utilities (generally not for sample processing)
        ProcessDictionary["Exercise all Opened valves"] = ExerciseAllValves;
        ProcessDictionary["Close all Opened valves"] = CloseAllValves;
        ProcessDictionary["Exercise all LN Manifold valves"] = ExerciseLNValves;
        ProcessDictionary["Close all LN Manifold valves"] = CloseLNValves;
        ProcessDictionary["Calibrate all multi-turn valves"] = CalibrateRS232Valves;
        ProcessDictionary["Open all multi-turn valves"] = OpenRS232Valves;
        ProcessDictionary["Measure MC volume (KV in MCP1)"] = MeasureVolumeMC;
        ProcessDictionary["Measure valve volumes (plug in MCP1)"] = MeasureValveVolumes;
        ProcessDictionary["Measure remaining chamber volumes"] = MeasureRemainingVolumes;
        ProcessDictionary["Check GR H2 density ratios"] = CalibrateGRH2;
        ProcessDictionary["Calibrate VP He initial manifold pressure"] = CalibrateVPHeP0;
        ProcessDictionary["Measure Extraction efficiency"] = MeasureExtractEfficiency;
        ProcessDictionary["Measure IP collection efficiency"] = MeasureIpCollectionEfficiency;
        Separators.Add(ProcessDictionary.Count);

        // Test functions
        ProcessDictionary["Test"] = Test;
    }

    #region Process Control Parameters
    #endregion Process Control Parameters

    #region Process Control Properties
    #endregion Process Control Properties

    #region Process Steps
    #endregion Process Steps

    #endregion Process Management

    #region Test functions
    void ValvePositionDriftTest()
    {
        var v = FirstOrDefault<RS232Valve>();
        var pos = v.ClosedValue / 2;
        var op = new ActuatorOperation()
        {
            Name = "test",
            Value = pos,
            Incremental = false
        };
        v.ActuatorOperations.Add(op);

        v.DoWait(op);

        //op.Incremental = true;
        var rand = new Random();
        for (int i = 0; i < 100; i++)
        {
            op.Value = pos + rand.Next(-15, 16);
            v.DoWait(op);
        }
        op.Value = pos;
        op.Incremental = false;
        v.DoWait(op);

        v.ActuatorOperations.Remove(op);
    }

    protected override void MeasureRemainingVolumes()
    {
        Find<VolumeCalibration>("MCP1, MCP2").Calibrate();
        Find<VolumeCalibration>("Split, GM").Calibrate();
        Find<VolumeCalibration>("VTT, CT, IM").Calibrate();
        Find<VolumeCalibration>("VM").Calibrate();
    }

    void TestPort(IPort p)
    {
        for (int i = 0; i < 5; ++i)
        {
            p.Open();
            p.Close();
        }
        p.Open();
        WaitMinutes(5);
        p.Close();
    }

    // two minutes of moving the valve at a moderate pace
    void TestValve(IValve v)
    {
        SampleLog.Record($"Operating {v.Name} for 2 minutes");
        for (int i = 0; i < 24; ++i)
        {
            v.CloseWait();
            WaitSeconds(2);
            v.OpenWait();
            WaitSeconds(2);
        }
    }

    void TestUpstream(IValve v)
    {
        SampleLog.Record($"Checking {v.Name}'s 10-minute bump");
        v.OpenWait();
        WaitMinutes(5);     // empty the upstream side (assumes the downstream side is under vacuum)
        v.CloseWait();
        WaitMinutes(10);    // let the upstream pressure rise for 10 minutes
        v.OpenWait();       // how big is the pressure bump?
    }

    protected void CalibrateManualHeaters()
    {
        var tc = Find<IThermocouple>("tCal");
        CalibrateManualHeater(Find<IHeater>("hIP1CCQ"), tc);
    }

    protected virtual void TestGasSupplies()
    {
        //TestPressurize("H2.GM", 100);
        //TestPressurize("H2.GM", 900);
        //TestPressurize("He.MC", 80);
        //TestPressurize("He.GM", 800);
        //TestAdmit("He.GM", 800);
        //TestAdmit("He.IM", 800);
        //TestPressurize("CO2.MC", 75);
        TestPressurize("CO2.MC", 900);
        //TestAdmit("O2.IM", IMO2Pressure);
    }

    protected void TestValveRaceCondition()
    {
        var valves = FindAll<IValve>(v => v.IsOpened && !(v is RS232Valve));
        var step = ProcessStep.Start("Test valve race condition.");
        var substep = ProcessSubStep.Start("");
        for (int i = 0; i < 5000; i++)
        {
            substep.Update($"{i + 1} / 5000");
            var valve = randomValve();
            if (valve.IsClosed)
            {
                if (Random.Shared.Next(2) < 1)
                    valve.Open();
                else
                    valve.OpenWait();
            }
            else
            {
                if (Random.Shared.Next(2) < 1)
                    valve.Close();
                else
                    valve.CloseWait();
            }
            Task.Delay((Random.Shared.Next(4) + 1) * 500).Wait();
        }
        substep.End();

        valves.Open();

        IValve randomValve()
        {
            return valves[Random.Shared.Next(valves.Count)];
        }
    }

    protected override void Test()
    {
        //var gr6 = GM.Ports.Last();
        //var manifold = Manifold(gr6);
        //manifold.JoinToVacuum();
        //double leakRate = HoldForLeakTightness(manifold);
        //TestLog.Record($"{manifold.Name} leak rate test: {leakRate:0.00e0}");
        //return;

        //var gr6LeakRate = PortLeakRate(gr6);
        //return;

        //var gmLeakRate = SectionLeakRate(GM, 3e-5);
        //gr6.Open();
        //var gr6LeakRate = PortLeakRate(gr6);
        //Notify.WaitForOperator("Open the syringe valve.");
        //var syringeLeakRate = PortLeakRate(gr6);
        //Hacs.SystemLog.Record("Leak Rate Test\r\n" +
        //    $"\t{GM.Name}: {gmLeakRate:0.00e0} Torr L/sec\r\n" +
        //    $"\t{gr6.Name}: {gr6LeakRate:0.00e0} Torr L/sec\r\n" +
        //    $"\tSyringe: {syringeLeakRate:0.00e0} Torr L/sec");
        //GM.VacuumSystem.Evacuate();
        //return;

        //TestValveRaceCondition();
        //return;

        //var grs = new List<IHeater>()
        //{
        //    Find<IHeater>("hGR2"),
        //    Find<IHeater>("hGR4"),
        //    Find<IHeater>("hGR6"),
        //    //Find<IHeater>("hGR7"),
        //    //Find<IHeater>("hGR9"),
        //    //Find<IHeater>("hGR11")
        //}.ToArray();
        //PidStepTest(grs);
        //return;

        //CalibrateManualHeaters();
        //return;

        //var ips = new List<IInletPort>()
        //{
        //    Find<IInletPort>("IP1"),
        //};
        //ips.ForEach(ip => ip.QuartzFurnace.TurnOn());
        //WaitMinutes(10);
        //PidStepTest(ips.Select(ip => ip.SampleFurnace).Cast<IHeater>().ToArray());
        //ips.ForEach(ip => ip.QuartzFurnace.TurnOff());
        //return;

        VttWarmStepTest();
        return;


        //TestGasSupplies();
        //return;

        //FastOpenLine();
        //for (int i = 0; i < 100; ++i)
        //{
        //    //ExercisePorts(IM);
        //    //ExercisePorts(GM);

        //    //MC.PathToVacuum?.Open();     // Opens GM, too
        //    //VTT.PathToVacuum?.Open();
        //    //IM.PathToVacuum?.Open();
        //    //IM_CT.Open();
        //    //VTT_MC.Open();

        //    var list = FindAll<CpwValve>(v => v.IsOpened && !(v is RS232Valve));
        //    list.ForEach(v => 
        //    {
        //        v.CloseWait();
        //        v.OpenWait();
        //    });
        //    WaitMinutes(30);
        //}

        //for (int i = 0; i < 5; ++i)
        //{
        //    TestValve(Find<IValve>("vIML_IMC"));
        //    TestValve(Find<IValve>("vIMR_IMC"));

        //    TestValve(Find<IValve>("vIMC_CT"));
        //    TestValve(Find<IValve>("vCT_VTT"));
        //    TestValve(Find<IValve>("vVTT_MC"));
        //    TestValve(Find<IValve>("vMC_MCP1"));
        //    TestValve(Find<IValve>("vMC_MCP2"));
        //    TestValve(Find<IValve>("vMC_Split"));

        //    TestValve(Find<IValve>("vGML_GMC"));
        //    TestValve(Find<IValve>("vGMR_GMC"));

        //    TestValve(Find<IValve>("vIMC_VM"));
        //    TestValve(Find<IValve>("vCT_VM"));
        //    TestValve(Find<IValve>("vGMC_VM"));

        //}
        //return;

        //TestPort(Find<IPort>("IP2"));
        //TestPort(Find<IPort>("IP3"));
        //TestPort(Find<IPort>("IP4"));
        //TestPort(Find<IPort>("IP5"));
        //TestPort(Find<IPort>("IP6"));

        //TestPort(Find<IPort>("GR7"));
        //TestPort(Find<IPort>("GR8"));
        //TestPort(Find<IPort>("GR9"));
        //TestPort(Find<IPort>("GR10"));
        //TestPort(Find<IPort>("GR11"));
        //TestPort(Find<IPort>("GR12"));

        //MC.Evacuate(OkPressure);
        //TestValve(Find<IValve>("v_MCP0"));
        //return;

        //ProcessStep.Start("Simulating Sample Run");
        //Wait(10000);
        //ProcessStep.End();

        //Admit("O2", IM, null, IMO2Pressure);

        //var gs = Find<GasSupply>("H2.GM");
        //gs.Pressurize(100);
        //gs.Pressurize(900);

        //var gs = Find<GasSupply>("He.GM");
        //gs.Admit(800);
        //WaitSeconds(10);

        //var gs = Find<GasSupply>("He.IM");
        //gs.Destination.Evacuate(OkPressure);

        //gs.Admit(800);
        //WaitSeconds(10);
        //gs.Destination.Evacuate(OkPressure);

        //gs = Find<GasSupply>("He.MC");
        //gs.Destination.Evacuate(OkPressure);

        //gs.Pressurize(95);
        //WaitSeconds(10);
        //gs.Destination.Evacuate(OkPressure);

        //gs = Find<GasSupply>("CO2.MC");
        //gs.Destination.Evacuate(OkPressure);

        //gs.Pressurize(75);
        //WaitSeconds(10);
        //gs.Destination.Evacuate(OkPressure);

        //gs.Pressurize(1000);
        //WaitSeconds(10);
        //gs.Destination.Evacuate(OkPressure);

        //InletPort = Find<InletPort>("IP1");
        //AdmitIPO2();
        //Collect();

        //var grs = new List<IGraphiteReactor>();
        //grs.AddRange(GraphiteReactors.Where(gr => gr.Prepared));
        //CalibrateGRH2(grs);

        //var gr1 = Find<GraphiteReactor>("GR1");
        //var gr2 = Find<GraphiteReactor>("GR2");
        //GrGmH2(gr1, out ISection gm, out IGasSupply gs);
        //gr1.Open();
        //gr2.Open();
        //gm.Evacuate(OkPressure);
        //gr1.Close();
        //gr2.Close();

        //gs.Pressurize(IronPreconditionH2Pressure);

        //var p1 = gm.Manometer.WaitForAverage(60);
        //gr1.Open();
        //WaitSeconds(10);
        //gr1.Close();
        //WaitSeconds(10);
        //var p2 = gm.Manometer.WaitForAverage(60);
        //SampleLog.Record($"dpGM for GR1: {p1:0.00} => {p2:0.00}");

        //p1 = gm.Manometer.WaitForAverage(60);
        //gr2.Open();
        //WaitSeconds(10);
        //gr2.Close();
        //WaitSeconds(10);
        //p2 = gm.Manometer.WaitForAverage(60);
        //SampleLog.Record($"dpGM for GR2: {p1:0.00} => {p2:0.00}");

        // Test CTFlowManager
        // Control flow valve to maintain constant downstream pressure until flow valve is fully opened.
        //SampleLog.Record($"Bleed pressure: {FirstTrapBleedPressure} Torr");
        //Bleed(FirstTrap, FirstTrapBleedPressure);

        // Open flow bypass when conditions allow it without producing an excessive
        // downstream pressure spike. Then wait for the spike to be evacuated.
        var substep = ProcessSubStep.Start("Wait for remaining pressure to bleed down");
        WaitFor(() => IM.Pressure - FirstTrap.Pressure < FirstTrapFlowBypassPressure);
        FirstTrap.Open();   // open bypass if available
        WaitFor(() => FirstTrap.Pressure < FirstTrapEndPressure);
        substep.End();


        //VolumeCalibrations["GR1, GR2"]?.Calibrate();
        //return;
    }

    #endregion Test functions
}