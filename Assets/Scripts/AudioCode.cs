using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCode : MonoBehaviour {
    public string GetHiHat() {
        return @"
            // this synchronizes to period
            1.5::second => dur T;
            T - (now % T) => now;

            SndBuf beat => NRev reverb => Pan2 p => dac;
            //0.7 => beat.gain;
            0.05 => reverb.mix;
            1.05 => beat.rate;
            -0.25 => p.pan;

            me.dir() + ""JK_HH_01.wav"" => string hiHat1;

            while(true) {
                0 => beat.pos;
                hiHat1 => beat.read;
                T/4 => now;
                
                0 => beat.pos;
                T/6 => now;
                
                0 => beat.pos;
                T/12 => now;
            }";
    }

    public string GetClick() {
        return @"
            // this synchronizes to period
            1.5::second => dur T;
            T - (now % T) => now;

            SndBuf beat => Pan2 bPan => dac;
            0.7 => beat.gain;
            SndBuf clap => Pan2 cPan => dac;
            0.1 => clap.gain;
            1.5 => clap.rate;
            .25 => cPan.pan => bPan.pan;

            me.dir() + ""click_04.wav"" => string click;
            me.dir() + ""clap_01.wav"" => string clap1;

            T/2 => now;

            while(true) {
                0 => beat.pos;
                click => beat.read;
                0 => clap.pos;
                clap1 => clap.read;
                T => now;
            }";
    }

    public string GetKick() {
        return @"
            // this synchronizes to period
            1.5::second => dur T;
            T - (now % T) => now;

            SndBuf beat => Pan2 p => dac;
            0.25 => beat.gain;
            0.25 => p.pan;

            me.dir() + ""kick_02.wav"" => string kick;

            while(true) {
                0 => beat.pos;
                kick => beat.read;
                T => now;
            }";
    }

    public string GetBass() {
        return @"
            // this synchronizes to period
            1.5::second => dur T;
            T - (now % T) => now;

            SinOsc s => dac;

            [30, 32, 34, 36] @=> int notes[];

            //Std.mtof(30) => s.freq;
            0.2 => s.gain;

            while(true) {
                Std.mtof(notes[1]) => s.freq;
                T => now;
                
                Std.mtof(notes[2]) => s.freq;
                T => now;
                
                Std.mtof(notes[1]) => s.freq;
                T => now;
                
                Std.mtof(notes[2]) => s.freq;
                T => now;
                
                Std.mtof(notes[3]) => s.freq;
                T => now;
                
                Std.mtof(notes[1]) => s.freq;
                T => now;
                
                Std.mtof(notes[3]) => s.freq;
                T => now;
                
                Std.mtof(notes[2]) => s.freq;
                T => now;
            }";
    }

    public string GetStrings() {
        return @"
            // this synchronizes to period
            1.5::second => dur T;
            T - (now % T) => now;

            Mandolin m => dac;

            std.mtof(64) => m.freq;
            .02 => m.stringDetune;
            0.6 => m.bodySize;
            .2 => m.gain;

            [62, 64, 66, 68, 70] @=> int scale[];

            while(true) {
                PlayNote(scale[2], T);
                PlayNote(scale[1], T/2);
                PlayNote(scale[3], T/4);
                PlayNote(scale[1], T/4);
                PlayNote(scale[2], T/2);
                PlayNote(scale[1], T/2); //3T
                
                PlayNote(scale[1], T/4);
                PlayNote(scale[0], T/4);
                PlayNote(scale[1], T/4);
                PlayNote(scale[2], T/2);
                PlayNote(scale[3], T/4);
                PlayNote(scale[4], T/2);
                PlayNote(scale[3], T);
                2*T => now;
            }

            function void PlayNote(int note, dur time) {
                std.mtof(note) => m.freq;
                0.5 => m.pluck;
                time => now;
            }";
    }

    public string GetClock() {
        return @"
            // this synchronizes to period
            1.5::second => dur T;
            T - (now % T) => now;

            SndBuf clock => dac;
            0.1 => clock.gain;

            me.dir() + ""Tick.wav"" => string clickFile;

            T/2 => now;

            while(true) {
                0 => clock.pos;
                clickFile => clock.read;
                T*(4/3) => now;
            }";
    }

    public string GetDrip() {
        return @"
            // this synchronizes to period
            1.5::second => dur T;
            T - (now % T) => now;

            SndBuf drop => Pan2 p => dac;
            0.5 => drop.gain;

            me.dir() + ""droplet.wav"" => string dropFile;

            [T*2, T*1.5, T, T*(3/2), T/2, T/3, T/4] @=> dur delays[];
            [0, 1, 2, 3, 3, 4, 4, 4, 5, 5, 6] @=> int delayScale[];
            while(true) {
                0 => drop.pos;
                Std.rand2f(-1.0, 1.0) => p.pan;
                Std.rand2f(0.75, 1.25) => drop.rate;
                dropFile => drop.read;
                delays[delayScale[Std.rand2(0, delayScale.size()-1)]] => now;
            }";
    }

    public string GetAlarm() {
        return @"
        ";
    }
}
