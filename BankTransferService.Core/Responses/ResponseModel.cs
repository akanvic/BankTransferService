using System;

namespace BankTransferService.Core.Responses
{
    public class ResponseModel/*<T> where T : IComparable*/
    {
        public int State { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }

        //public ResponseModel<T> Message(T ret)
        //{
        //    if (ret == null)
        //        return (new ResponseModel<T>{ State = 0, Msg = "was not added, Something went wrong" });
        //    return (new ResponseModel<T> { State = 1, Msg = "Role Successfuly Added", Data = ret });
        //}


        /// <summary>
        /// Instantiate the model in your Service class
        /// use the instance to call the method based on the CRUD Scenario
        /// 
        /// the second takes a string which is name of the model object you implement the crud on
        /// See Example
        /// for Add method, use
        /// ResponseModel res = new ResponseModel();  //Instantiate the model above the service class to 
        /// use it for all Cruds in the service class you're implementing
        /// res.AMessage(ret, "Role");
        /// </summary>
        /// <param name="ret">the first argument takes your object variable</param>
        /// <param name="objName"></param>
        /// <returns></returns>
        /// 




        //Call this method for Add types in service class
        public ResponseModel AMessage(object ret, string objName)
        {
            if ((int)ret < 1)
                return (new ResponseModel { State = 0, Msg = $"Unable to add {objName}, Something went wrong" });
            return (new ResponseModel { State = 1, Msg = $"{objName} was successfully added", Data = ret });
        }




        //Call this method for Update types in service class
        public ResponseModel UMessage(object ret, string objName)
        {
            if ((int)ret < 1)
                return (new ResponseModel { State = 0, Msg = $"Could not Update {objName}, Something went wrong" });
            return (new ResponseModel
            {
                State = 1,
                Msg = $"{objName} was successfully updated",
                Data = ret
            });
        }






        //Call this method for GetById and GetAll types in service class
        public ResponseModel NMessage(object ret, string objName)
        {
            if (ret == null)
                return (new ResponseModel { State = 0, Msg = $"No Record for {objName} found" });
            return (new ResponseModel { State = 1, Msg = $"{objName} successfully retrieved", Data = ret });
        }

        //Call this method for Login
        public ResponseModel LoginStatus(object ret, string objName)
        {
            if (ret == null)
                return (new ResponseModel { State = 0, Msg = $" Login Failed! Incorrect Email or Password" });
            return (new ResponseModel { State = 1, Msg = $"Login Successful", Data = ret });
        }




        //Call this method for Delete types in service class
        public ResponseModel DMessage(object ret, string objName)
        {
            if ((int)ret < 1)
                return (new ResponseModel { State = 0, Msg = $"Could not delete {objName}, Something went wrong" });
            return (new ResponseModel
            {
                State = 1,
                Msg = $"{objName} was successfully deleted",
                Data = ret
            });
        }


        public ResponseModel Approval(object ret, string objName)
        {
            if ((int)ret < 1)
                return (new ResponseModel { State = 0, Msg = $"{objName} approval failed, Something went wrong" });
            return (new ResponseModel
            {
                State = 1,
                Msg = $"{objName} successfully approved",
                Data = ret
            });
        }

        public ResponseModel Disapproval(object ret, string objName)
        {
            if ((int)ret < 1)
                return (new ResponseModel { State = 0, Msg = $"{objName} disapproval failed, Something went wrong" });
            return (new ResponseModel
            {
                State = 1,
                Msg = $"{objName} successfully Disapproved",
                Data = ret
            });
        }


        //Call this method for Upload types in service class
        public ResponseModel Upload(object ret, string objName)
        {
            if ((int)ret < 1)
                return (new ResponseModel { State = 0, Msg = $"Unable to Upload {objName}, Something went wrong" });
            return (new ResponseModel { State = 1, Msg = $"{objName} was successfully Uploaded", Data = ret });
        }

        //Call this method for Upload types in service class
        public ResponseModel Download(object ret, string objName)
        {
            if (ret == null)
                return (new ResponseModel { State = 0, Msg = $"Unable to Download {objName}, Something went wrong" });
            return (new ResponseModel { State = 1, Msg = $"{objName} was successfully Downloaded", Data = ret });
        }
        //Call this method for Change Passowrd types in service class
        public ResponseModel ChangePassword(object ret, string objName)
        {
            if ((int)ret < 1)
                return (new ResponseModel { State = 0, Msg = $"Unable to change password for {objName}, Something went wrong" });
            return (new ResponseModel { State = 1, Msg = $"password was changed successfully for {objName} ", Data = ret });
        }

        public ResponseModel Terminated(object ret, string objName)
        {
            if ((int)ret < 1)
                return (new ResponseModel { State = 0, Msg = $"{objName} Termination failed, Something went wrong" });
            return (new ResponseModel
            {
                State = 1,
                Msg = $"{objName} successfully Terminated",
                Data = ret
            });
        }


        

    }


    public static class ExtensionModel
    {
        public static string GetNullMessage(this string value)
        {
            string nullMessage;
            if (String.IsNullOrEmpty(value) || value == null)
            {
                nullMessage = "Record does not exist or Empty Value";
                return nullMessage;
            }
            return value;
        }

        public static string GetDataError(this string value, string dataName)
        {
            string nullMessage;
            if (String.IsNullOrEmpty(value) || value == null)
            {
                nullMessage = $"Invalid {dataName}";
                return nullMessage;
            }
            return value;
        }
    }


}

