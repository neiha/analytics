using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;

namespace analytics.Models.DAO
{
    public class DAO<T> where T :class
    {
        public T GetById(object id)
        {
            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                T o = session.Get<T>(id);
                return o;
            }
        }
        public T Insert(T o)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    using (ITransaction tx = session.BeginTransaction())
                    {
                        session.Save(o);
                        tx.Commit();
                    }
                }
                return o;
            }

            #region UpdateMethods

            public T Update(T o)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    using (ITransaction tx = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(o);
                        tx.Commit();
                        return o;
                    }
                }
            }
            /*
             * Update database by a query and a dictionary of parameters
            */
            protected int Update(string query, Dictionary<string, object> parameters)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    ISQLQuery iquery = session.CreateSQLQuery(query);
                    foreach (KeyValuePair<string, object> param in parameters)
                        iquery.SetParameter(param.Key, param.Value);
                    return iquery.ExecuteUpdate();
                }
            }
            /*
             * Update all the objects in the list in a single transaction
            */
            public List<T> Update(List<T> list)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    using (ITransaction tx = session.BeginTransaction())
                    {
                        foreach (T o in list)
                            session.SaveOrUpdate(o);
                        tx.Commit();
                    }
                }
                return list;
            }
            /*
             * Update all the objects of the list on a single transaction after executing a customizable deleteQuery
            */
            protected List<T> Update(List<T> list, string deleteQuery)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    using (ITransaction tx = session.BeginTransaction())
                    {
                        session.CreateQuery(deleteQuery).ExecuteUpdate();
                        foreach (T o in list)
                            session.SaveOrUpdate(o);
                        tx.Commit();
                    }
                }
                return list;
            }

            #endregion

            #region Delete Methods

            public void Delete(T o)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    using (ITransaction tx = session.BeginTransaction())
                    {
                        session.Delete(o);
                        tx.Commit();
                    }
                }
            }
            /*
             * Delete from database by a query provided by the user
            */
            protected void Delete(string query)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    using (ITransaction tx = session.BeginTransaction())
                    {
                        session.CreateSQLQuery(query).ExecuteUpdate();
                        tx.Commit();
                    }
                }
            }
            protected void Delete(string query, Dictionary<string, object> parameters)
            {

                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    using (ITransaction tx = session.BeginTransaction())
                    {
                        ISQLQuery iquery = session.CreateSQLQuery(query);
                        foreach (KeyValuePair<string, object> param in parameters)
                            iquery.SetParameter(param.Key, param.Value);
                        iquery.ExecuteUpdate();
                        tx.Commit();
                    }
                }
            }

            #endregion

            #region List Methods

            /*
             * return a list of objects according to the given HQL query
            */
            protected List<T> List(String hqlQuery)
            {
                return new List<T>(List(hqlQuery, new Dictionary<string, object>()));
            }

            protected List<T> List(String hqlQuery, Dictionary<string, object> parameters)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    IQuery iquery = session.CreateQuery(hqlQuery);
                    foreach (KeyValuePair<string, object> param in parameters)
                        iquery.SetParameter(param.Key, param.Value);
                    List<T> Lista = new List<T>(iquery.List<T>());
                    return Lista;
                }
            }
        public List<T> ListAll()
        {
            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                List<T> o = new List<T>(session.CreateCriteria<T>().List<T>());
                return o;
            }
        }
        public List<T> ListActive()
        {
            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                List<T> o = new List<T>(session.CreateCriteria<T>().Add(Restrictions.Eq("Activo", true)).List<T>());
                return o;
            }
        }

        /*
         * return a list of objects according to the given SQL query. Columns must remain with their original names
        */
        protected List<T> ListFromSQL(String query)
            {
                return new List<T>(ListFromSQL(query, new Dictionary<string, object>()));
            }

            protected List<T> ListFromSQL(String query, Dictionary<string, object> parameters)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    ISQLQuery iquery = session.CreateSQLQuery(query);
                    iquery.AddEntity(typeof(T));
                    foreach (KeyValuePair<string, object> param in parameters)
                        iquery.SetParameter(param.Key, param.Value);
                    List<T> Lista = new List<T>(iquery.List<T>());
                    return Lista;
                }
            }
            protected List<U> ListFromSQL<U>(String query)
            {
                /*
                 * return a list of objects according to the given SQL query, specifying the Type of Object to return. 
                 * Column names of the result of the query must match with the property names of the object
                */
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    ISQLQuery iquery = session.CreateSQLQuery(query);
                    iquery.SetResultTransformer(Transformers.AliasToBean(typeof(U)));
                    List<U> Lista = new List<U>(iquery.List<U>());
                    return Lista;
                }

            }
            #endregion

            #region GetObject Methods
            protected T GetObject(String query)
            {
                return GetObject(query, new Dictionary<string, object>());
            }

            protected T GetObject(String query, Dictionary<string, object> parameters)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    IQuery iquery = session.CreateQuery(query);
                    foreach (KeyValuePair<string, object> param in parameters)
                        iquery.SetParameter(param.Key, param.Value);
                    T obj = iquery.UniqueResult<T>();
                    return obj;
                }
            }


            protected T GetObjectFromSQL(String query, Type resultType)
            {
                return GetObjectFromSQL(query, new Dictionary<string, object>());
            }
            protected U GetObjectFromSQL<U>(String query, Dictionary<string, object> parameters)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    ISQLQuery iquery = session.CreateSQLQuery(query);
                    iquery.SetResultTransformer(Transformers.AliasToBean(typeof(U)));
                    foreach (KeyValuePair<string, object> param in parameters)
                        iquery.SetParameter(param.Key, param.Value);
                    U obj = iquery.UniqueResult<U>();
                    return obj;
                }
            }

            protected T GetObjectFromSQL(String query, Dictionary<string, object> parameters)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    ISQLQuery iquery = session.CreateSQLQuery(query);
                    iquery.AddEntity(typeof(T));
                    foreach (KeyValuePair<string, object> param in parameters)
                        iquery.SetParameter(param.Key, param.Value);
                    T obj = iquery.UniqueResult<T>();
                    return obj;
                }
            }

            #endregion

            protected U GetValue<U>(string query)
            {
                return GetValue<U>(query, new Dictionary<string, object>());
            }
            protected U GetValue<U>(string query, Dictionary<string, object> parameters)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    IQuery iquery = session.CreateSQLQuery(query);
                    foreach (KeyValuePair<string, object> param in parameters)
                        iquery.SetParameter(param.Key, param.Value);
                    return (U)iquery.UniqueResult();
                }

            }
        }
    }
