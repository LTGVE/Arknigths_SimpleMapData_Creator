using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Manager
{
    internal class LevelDataManager : MonoBehaviour
    {
        public static int enemiesCount = 0;
        public LevelData GameData { get; set; }
        public static bool hasInfectionFloor = false;
        void Start()
        {
        }
        public void loadLevelData()
        {
            hasInfectionFloor = false;
            initGame();
        }
        public  void initGame()
        {

            enemiesCount = 0;
            var waves = GameData.waves;
            float fragTime = 0;
            float fragTime2 = 0;
            for (int w = 0; w < waves.Count; w++)
            {
                fragTime += (float)waves[w].preDelay;
                fragTime2 += (float)waves[w].preDelay;
                for (int f = 0; f < waves[w].fragments.Count; f++)
                {
                    var fragment = waves[w].fragments[f];
                    List<actions> actions = fragment.actions.OrderBy(action => action.preDelay).ToList();
                    float maxTime = 0;
                    for (int i = 0; i < actions.Count; i++)
                    {
                        var action = actions[i];
                        if (action.actionType != "SPAWN")
                        {
                            if (action.actionType == "PREVIEW_CURSOR")
                            {
                                action.spawnTime.Add(fragTime + (float)action.preDelay);
                                action.WaveSpawnTime.Add(fragTime2 + (float)action.preDelay);
                            }
                            if (action.actionType == "DISPLAY_ENEMY_INFO")
                            {
                                action.spawnTime.Add(fragTime + (float)action.preDelay);
                                action.WaveSpawnTime.Add(fragTime2 + (float)action.preDelay);

                            }
                        }
                        else
                        {
                            enemiesCount += action.count;
                            if (action.count > 1)
                            {
                                for (int a = 1; a < action.count + 1; a++)
                                {
                                    var time = (float)action.preDelay + (float)((a - 1) * action.interval);
                                    action.spawnTime.Add(fragTime + (float)fragment.preDelay + time);
                                    action.WaveSpawnTime.Add(fragTime2 + (float)fragment.preDelay + time);
                                    maxTime = Math.Max(maxTime, time);
                                }
                            }
                            else
                            {
                                action.spawnTime.Add(fragTime + (float)fragment.preDelay + (float)action.preDelay);
                                action.WaveSpawnTime.Add(fragTime2 + (float)fragment.preDelay + (float)action.preDelay);
                                maxTime = Math.Max(maxTime, (float)action.preDelay);
                            }
                            string t = string.Join(",", action.spawnTime);
                            string wt = string.Join(",", action.WaveSpawnTime);
                            //Debug.WriteLine($"wave {w} fragment {f} action {i} count {action.count} Count {enemiesCount} interval {action.interval} time {t} waveTime {wt}");
                        }
                    }
                    fragTime += (float)fragment.preDelay + maxTime;
                    fragTime2 += (float)fragment.preDelay + maxTime;
                }
                fragTime2 = 0;
                fragTime += 5;
            }
        }
        public static bool IsAllTheSame(List<actions> actions)
        {
            if (actions == null || actions.Count <= 1)
            {
                return true;
            }
            string firstPreDelay = actions[0].preDelay.ToString();
            for (int i = 1; i < actions.Count; i++)
            {
                if (actions[i].preDelay.ToString() != firstPreDelay)
                {
                    return false;
                }
            }

            return true;
        }
    }
        public class LevelData
        {
            public options options { get; set; }
            public mapdata mapdata { get; set; }
            public List<route> routes { get; set; }
            public List<enemyDBRef> enemyDBRefs { get; set; }
            public List<wave> waves { get; set; }
            public predefines predefines { get; set; }
        }
        public class options
        {
            public int characterLimit { get; set; }
            public int maxLifePoint { get; set; }
            public int initialCost { get; set; }
            public int maxCost { get; set; }
            public double costIncreaseTime { get; set; }
            public double maxPlayTime { get; set; }

        }
        public class mapdata
        {
            public List<List<int>> map { get; set; }
            public List<tile> tiles { get; set; }

        }

        public class tile
        {
            public string tileKey { get; set; }
            public string heightType { get; set; }
            public string buildableType { get; set; }
            public string passableMask { get; set; }
            public string playerSideMask { get; set; }
            public List<blackboard> blackboard { get; set; }
            public object? effects { get; set; }
        }
        public class blackboard
        {
            public string? key { get; set; }
            public object value { get; set; }
            public object valueStr { get; set; }
        }

        public class enemyDBRef
        {
            public bool useDB { get; set; }
            public string id { get; set; }
            public int level { get; set; }
            public object? overwrittenData { get; set; }
        }

        //
        //
        //路径&波次
        //
        //

        //路径

        public class route
        {
            public string motionMode { get; set; }
            public startPosition startPosition { get; set; }
            public endPosition endPosition { get; set; }
            public position spawnRandomRange { get; set; }
            public position spawnOffset { get; set; }
            public List<CPoint> checkpoints { get; set; }
            public bool allowDiagonalMove { get; set; }
            public bool visitEveryTileCenter { get; set; }
            public bool visitEveryNodeCenter { get; set; }
            public bool visitEveryCheckPoint { get; set; }
        }

        public class startPosition : position
        {
            public startPosition(float row, float col) : base(row, col)
            {
                this.row = row;
                this.col = col;
            }
        }
        public class endPosition : position
        {
            public endPosition(float row, float col) : base(row, col)
            {
                this.row = row;
                this.col = col;
            }
        }

        public class CPoint
        {
            public CPoint(string type, double time, position position, reachOffset reachOffset)
            {
                this.type = type;
                this.time = time;
                this.position = position;
                this.reachOffset = reachOffset;
            }
            public string type { get; set; }
            public double time { get; set; }
            public position position { get; set; }
            public reachOffset reachOffset { get; set; }
            public bool checkCentre { get; set; } = true;
            public bool randomizeReachOffset { get; set; }
            public float reachDistance { get; set; }
            public Vector2 pos { get; set; }
            public override string ToString()
            {
                return $"({pos})";
            }

        }


        public class position
        {
            public position(float row, float col)
            {
                this.row = row;
                this.col = col;
            }
            public float? row { get; set; }

            public float? col { get; set; }
        }
        public class reachOffset
        {
            public reachOffset(double row, double col)
            {
                y = row;
                x = col;
            }
            public double? x { get; set; }
            public double? y { get; set; }
        }


        //波次

        public class wave
        {
            public double preDelay { get; set; }
            public double postDelay { get; set; }
            public double maxTimeWattingForNextWave { get; set; }
            public List<fragments> fragments { get; set; }
            public string advancedWaveTag { get; set; }
        }

        public class fragments
        {
            public double preDelay { get; set; }
            public List<actions> actions { get; set; }

        }
        public class actions
        {
            public List<float> spawnTime { get; set; } = new List<float>();
        public List<float> WaveSpawnTime { get; set; } = new List<float>();

        public string actionType { get; set; }
            public bool managedByScheduler { get; set; }
            public string key { get; set; }
            public int count { get; set; }
            public double preDelay { get; set; }
            public double interval { get; set; }
            public int routeIndex { get; set; }
            public bool blockFragment { get; set; }
            public bool autoPreviewRoute { get; set; }
            public bool autoDisplayEnemyInfo { get; set; }
            public bool isUnharmfulAndAlwaysCountAsKilled { get; set; }
            public object hiddenGroup { get; set; }
            public object randomSpawnGroupKey { get; set; }
            public object randomSpawnGroupPackKey { get; set; }
            public string randomType { get; set; }
            public string refreshType { get; set; }
            public int weight { get; set; }
            public bool dontBlockWave { get; set; }
            public bool isValid { get; set; }
            public object extraMeta { get; set; }
            public object actionId { get; set; }
            public bool isSpawnd { get; set; } = false;
        }
        public class predefines
        {
            public List<tokenInsts> tokenInsts { get; set; }
        }
        public class tokenInsts
        {
            public string alias { get; set; }
            public string direction { get; set; }
            public position position { get; set; }
            public inst inst { get; set; }

        }
        public class inst
        {
            public string characterKey { get; set; }
            public int level { get; set; }

        }
        public class EnemiesData
        {
            public List<enemies> enemies { get; set; }
        }
        public class enemies
        {
            public string Key { get; set; }
            public List<value> Value { get; set; }
        }
        public class value
        {
            public int level { get; set; }
            public enemyData enemyData { get; set; }

        }
        public class enemyData
        {
            public data name { get; set; }
            public data description { get; set; }
            public data prefabKey { get; set; }
            public attributes attributes { get; set; }
            public data motion { get; set; }
            public List<talent>? talentBlackboard { get; set; }

        }
        public class talent
        {
            public string key { get; set; }
            public object value { get; set; }
            public string valueStr { get; set; }
        }
        public class attributes
        {
            public data maxHp { get; set; }
            public data moveSpeed { get; set; }
        }

        public class data
        {
            public bool m_defined { get; set; }
            public object m_value { get; set; }
        }
}