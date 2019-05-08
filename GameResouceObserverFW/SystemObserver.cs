﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameResouceManagerFW
{
    class SystemObserver
    {
        //コンピュータ名
        //"."はローカルコンピュータを表す
        //コンピュータ名は省略可能（省略時は"."）
        string machineName = ".";
        //カテゴリ名
        string categoryName = "Processor";
        //カウンタ名
        string counterName = "% Processor Time";
        //インスタンス名
        string instanceName = "_Total";

        SystemObserver()
        {

            //カテゴリが存在するか確かめる
            if (!System.Diagnostics.PerformanceCounterCategory.Exists(
            this.categoryName, this.machineName))
            {
                Console.WriteLine("登録されていないカテゴリです。");
                return;
            }

            //カウンタが存在するか確かめる
            if (!System.Diagnostics.PerformanceCounterCategory.CounterExists(
                this.counterName, this.categoryName, this.machineName))
            {
                Console.WriteLine("登録されていないカウンタです。");
                return;
            }

            //PerformanceCounterオブジェクトの作成
            System.Diagnostics.PerformanceCounter pc =
                new System.Diagnostics.PerformanceCounter(
                categoryName, counterName, instanceName, machineName);
        }

        static void Main(string[] args)
        {


            //1秒おきに値を取得する
            for (int i = 0; i < 10; i++)
            {
                //計算された値を取得し、表示する
                Console.WriteLine(pc.NextValue());
                //1秒待機する
                System.Threading.Thread.Sleep(1000);
            }
        }
   
}