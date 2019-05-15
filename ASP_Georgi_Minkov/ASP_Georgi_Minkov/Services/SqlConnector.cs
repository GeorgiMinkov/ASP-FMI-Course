using Dapper;

using ASP_Georgi_Minkov.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Collections;

// using Dapper;

namespace ASP_Georgi_Minkov.Services
{
    public class SqlConnector
    {
        private Fotm root;
        private Dictionary<string, string> querry = new Dictionary<string, string>();
        public SqlConnector()
        {
            this.root = null;
        }

        public SqlConnector(Fotm root)
        {
            this.root = root;
        }

        public bool saveToDb()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=formula1;Integrated Security=True"))
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // TODO: put it in function
                        if (!isGroupsCreated("GR1"))
                        {
                            foreach (Group group in root.groups.groupsList)
                            {
                                connection.Execute($"insert into [Group] (code, text) values ('{group.groupCode}', N'{group.text}')", transaction: transaction);
                            }
                        }
                        else
                        {
                            // no need for exception because every xml contains that infromation
                        }

                        foreach (Track track in root.tracks.tracksList)
                        {
                            if (!isTrackInDb(track.id))
                            {
                                connection.Execute($"insert into Track (id, name, firstRaceAge, lapRecord, bestPilotId) values ('{track.id}', N'{track.name}', '{track.firstRace}', " +
                                    $"'{track.lapRecord}', '{track.bestPilotId}')", transaction: transaction);
                            }
                            else
                            {
                                // throw 
                                throw new Exception();
                            }
                        }

                        foreach (Team team in root.teams.teamsList)
                        {
                            foreach (Pilot pilot in team.pilots.pilotsList)
                            {
                                if (!isPilotInDb(pilot.id))
                                {
                                    connection.Execute($"insert into Pilot (id, name, nationality, number) values ('{pilot.id}', N'{pilot.name}', " +
                                        $"N'{pilot.nationality}', '{pilot.number}')", transaction: transaction);
                                }
                                else
                                {
                                    // exception no load and rollback
                                    throw new Exception();
                                }
                            }
                            
                            connection.Execute($"insert into PilotDuo (firstPilotId, secondPilotId) values ('{team.pilots.pilotsList.ElementAt(0).id}', '{team.pilots.pilotsList.ElementAt(1).id}')", transaction: transaction);

                            int pilotDuoId = connection.QuerySingle<int>($"select id from PilotDuo where firstPilotId = '{team.pilots.pilotsList.ElementAt(0).id}'", transaction: transaction);

                            connection.Execute($"insert into Team (id, name, titles, colour, pointsEarned, numberOfWins, numberOfPolePosition, teamChief, budget, firstTeamEntry, baseLocation," +
                                $"numberOfRaces, pilotDuoId, powerUnit, fastestPitStop, teamLogo, country, nickname, groupId, currency, trackEntered)" +
                                $"values ('{team.id}', N'{team.name}', '{team.titles}', N'{team.colour}', '{team.pointsEarned}', '{team.numberOfWins}', '{team.numberOfPolePositions}', N'{team.teamChief}', '{team.budget}', '{team.firstTeamEntry}', N'{team.baseLocation}', " +
                                $"'{team.numberOfRaces}', '{pilotDuoId}', N'{team.powerUnit}', '{team.fastestPitStop}', '{team.teamLogo}', N'{team.country}', N'{team.nickname}', '{team.group}', '{team.currency}', N'{team.trackEntered}')", transaction: transaction);
                        }


                       transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        private bool isGroupsCreated(string groupId) // , IDbConnection connection, IDbTransaction transaction
        {
            // return connection.Execute($"Select count(id) FROM Group where id = '{groupId}'", transaction: transaction) == 0;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=formula1;Integrated Security=True"))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int hasDuplicates = connection.QuerySingle<int>($"select count(code) from [Group] where code = '{groupId}'", transaction: transaction);

                        return hasDuplicates == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        /**
         * Check if team exist in Team table, if
         * id correspond in one from the table 
         * we do not insert team entity in table
         * */
        private bool isTeamInDb(string teamId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=formula1;Integrated Security=True"))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int hasDuplicates = connection.QuerySingle<int>($"select count(id) from Team where id = '{teamId}'", transaction: transaction);

                        return hasDuplicates == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        //private bool isPilotDuoInDb(int pilotDuoId)
        //{
        //    using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=formula1;Integrated Security=True"))
        //    {
        //        connection.Open();
        //        using (IDbTransaction transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                int hasDuplicates = connection.QuerySingle<int>($"select count(id) from Pilot where id = '{pilotDuoId}'", transaction: transaction);

        //                return hasDuplicates == 0 ? false : true;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //    }
        //}

        private bool isPilotInDb(int pilotId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=formula1;Integrated Security=True"))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int hasDuplicates = connection.QuerySingle<int>($"select count(id) from Pilot where id = '{pilotId}'", transaction: transaction);

                        return hasDuplicates == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private bool isTrackInDb(int trackId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=formula1;Integrated Security=True"))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int hasDuplicates = connection.QuerySingle<int>($"select count(id) from Track where id = '{trackId}'", transaction: transaction);

                        return hasDuplicates == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        //private bool init()
        //{
        //    querry.Add("team", "");
        //    querry.Add("pilot", "");
        //    querry.Add("track", "");
        //    querry.Add("pilot", "");
        //    querry.Add("pilot", "");
        //}
        //private bool isEntityInDb(int entityId)
        //{

        //}
    }
}