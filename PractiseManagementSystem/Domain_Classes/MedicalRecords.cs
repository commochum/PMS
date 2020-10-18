using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseManagementSystem.Domain_Classes
{
    class MedicalRecords
    {
        string medicalRecordsId;
        string medPatientId;
        string medDoctorId;
        string medEmployeeId;
        string medAssignedDoctor;
        DateTime consultationDate;
        string bloodPressure;
        string bloodType;
        double weight;
        double height;
        string familyHistory;
        string pastHealthHistory;
        string pregnant;
        string isSmoking;
        string hasDiabetes;
        string hasHighBP;
        string hasHighCholesterol;
        string hasHeartProblem;
        string hasAsthma;
        string hasEpilepsy;
        string hasPneumonia;
        string hasHepatitis;
        string hasUlcer;
        string hasLeukemia;
        string hasTB;
        string hasHIV;
        string chiefComplaint;
        string presentIllness;
        string medicationTaken;
        string diagnosis;
        string currentRegimen;
        string prescription;
        int recordedByUserId;
        DateTime recordedDate;
        int updatedByUserId;
        DateTime updatedDate;

        protected ConnectionFactory connFactory = new ConnectionFactory();
        string message = null;

        DataTable dt = new DataTable();

        public string MedicalRecordsId
        {

            get
            {
                return medicalRecordsId;
            }
            set
            {
                this.medicalRecordsId = value;
            }

        }

        public string MedPatientId
        {

            get
            {
                return medPatientId;
            }
            set
            {
                this.medPatientId = value;
            }

        }
        public string MedEmployeeId
        {

            get
            {
                return medEmployeeId;
            }
            set
            {
                this.medEmployeeId = value;
            }

        }
        public string MedDoctorId
        {

            get
            {
                return medDoctorId;
            }
            set
            {
                this.medDoctorId = value;
            }

        }
        public string MedAssignedDoctor
        {

            get
            {
                return medAssignedDoctor;
            }
            set
            {
                this.medAssignedDoctor = value;
            }

        }
        public string BloodPressure
        {

            get
            {
                return bloodPressure;
            }
            set
            {
                this.bloodPressure = value;
            }

        }
        public string BloodType
        {

            get
            {
                return bloodType;
            }
            set
            {
                this.bloodType = value;
            }

        }

        public string FamilyHistory
        {

            get
            {
                return familyHistory;
            }
            set
            {
                this.familyHistory = value;
            }

        }
        public string ChiefComplaint
        {

            get
            {
                return chiefComplaint;
            }
            set
            {
                this.chiefComplaint = value;
            }

        }
        public string PresentIllness
        {

            get
            {
                return presentIllness;
            }
            set
            {
                this.presentIllness = value;
            }

        }

        public string MedicationTaken
        {

            get
            {
                return medicationTaken;
            }
            set
            {
                this.medicationTaken = value;
            }

        }
        public string Diagnosis
        {

            get
            {
                return diagnosis;
            }
            set
            {
                this.diagnosis = value;
            }

        }
        public string CurrentRegimen
        {

            get
            {
                return currentRegimen;
            }
            set
            {
                this.currentRegimen = value;
            }

        }
        public string Prescription
        {

            get
            {
                return prescription;
            }
            set
            {
                this.prescription = value;
            }

        }
        public string PastHealthHistory {

            get
            {
                return pastHealthHistory;
            }
            set
            {
                this.pastHealthHistory = value;
            }

        }
        public string IsSmoking
        {
            get
            {
                return isSmoking;
            }
            set
            {
                this.isSmoking = value;
            }
        }
        public string HasDiabetes
        {
            get
            {
                return hasDiabetes;
            }
            set
            {
                this.hasDiabetes = value;
            }
        }
        public string HasAsthma
        {
            get
            {
                return hasAsthma;
            }
            set
            {
                this.hasAsthma = value;
            }
        }
        public string HasEpilepsy
        {
            get
            {
                return hasEpilepsy;
            }
            set
            {
                this.hasEpilepsy = value;
            }
        }
        public string HasHighBP
        {
            get
            {
                return hasHighBP;
            }
            set
            {
                this.hasHighBP = value;
            }
        }
        public string HasHighCholesterol
        {
            get
            {
                return hasHighCholesterol;
            }
            set
            {
                this.hasHighCholesterol = value;
            }
        }
        public string HasHIV
        {
            get
            {
                return hasHIV;
            }
            set
            {
                this.hasHIV = value;
            }
        }
        public string HasHeartProblem
        {
            get
            {
                return hasHeartProblem;
            }
            set
            {
                this.hasHeartProblem = value;
            }
        }
        public string HasHepatitis
        {
            get
            {
                return hasHepatitis;
            }
            set
            {
                this.hasHepatitis = value;
            }
        }

        public string HasLeukemia
        {
            get
            {
                return hasLeukemia;
            }
            set
            {
                this.hasLeukemia = value;
            }
        }
        public string HasPneumonia
        {
            get
            {
                return hasPneumonia;
            }
            set
            {
                this.hasPneumonia = value;
            }
        }
        public string HasTB
        {
            get
            {
                return hasTB;
            }
            set
            {
                this.hasTB = value;
            }
        }
        public string HasUlcer
        {
            get
            {
                return hasUlcer;
            }
            set
            {
                this.hasUlcer = value;
            }
        }
        public Double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                this.weight = value;
            }
        }
        public Double Height
        {
            get
            {
                return height;
            }
            set
            {
                this.height = value;
            }
        }
        public string isPregnant
        {
            get
            {
                return pregnant;
            }
            set
            {
                this.pregnant = value;
            }
        }
        public DateTime ConsultationDate
        {
            get
            {
                return consultationDate;
            }
            set
            {
                this.consultationDate = value;
            }
        }
        public int RecordedByUserId
        {
            get
            {
                return recordedByUserId;
            }
            set
            {
                this.recordedByUserId = value;
            }
        }
        public int UpdatedByUserId
        {
            get
            {
                return updatedByUserId;
            }
            set
            {
                this.updatedByUserId = value;
            }
        }

        public DateTime RecordedDate
        {
            get
            {
                return recordedDate;
            }
            set
            {
                this.recordedDate = value;
            }
        }
        public DateTime UpdatedDate
        {
            get
            {
                return updatedDate;
            }
            set
            {
                this.updatedDate = value;
            }
        }

        internal string createPatientMedicalRecords()
        {
          string queryString = "SET DATEFORMAT dmy; INSERT INTO MedicalRecords(patientId,doctorId,employeeId,assignedDoctor,consultationDate,bloodPressure,bloodType,weight,height,familyHistory,pastHealthHistory,isSmoking,isPregnant," +
                "hasDiabetes,hasHighBP,hasHighCholesterol,hasHeartProblem,hasAsthma,hasEpilepsy,hasPneumonia, hasHepatitis,hasUlcer,hasLeukemia,hasTB,hasHIV," +
                "chiefComplaint,presentIllness,medicationTaken,diagnosis,currentRegimen,prescription,recordedDate,updatedDate) VALUES " +
                "(" + MedPatientId.Trim() +
                ", " + MedDoctorId.Trim() +
                ", (select employeeId from Doctor where doctorId = " + Convert.ToInt32(MedDoctorId) + ") " +
                ", '" + MedAssignedDoctor.Trim() +
                "', CONVERT(datetime, '" + ConsultationDate + "', 103)" +
                ", '" + BloodPressure.Trim() +
                "', '" + BloodType.Trim() +
                "', " + Weight +
                ", " + Height +
                ", '" + FamilyHistory.Trim() +
                "', '" + PastHealthHistory.Trim() +
                "', '" + isSmoking.Trim() +
                "', '" + isPregnant.Trim() +
                "', '" + HasDiabetes.Trim() +
                "', '" + HasHighBP.Trim() +
                "', '" + HasHighCholesterol.Trim() +
                "', '" + HasHeartProblem.Trim() +
                "', '" + HasAsthma.Trim() +
                "', '" + hasEpilepsy.Trim() +
                "', '" + HasPneumonia.Trim() +
                "', '" + HasHepatitis.Trim() +
                "', '" + HasUlcer.Trim() +
                "', '" + HasLeukemia.Trim() +
                "', '" + HasTB.Trim() +
                "', '" + HasHIV.Trim() +
                "', '" + ChiefComplaint.Trim() +
                "', '" + PresentIllness.Trim() +
                "', '" + MedicationTaken.Trim() +
                "', '" + Diagnosis.Trim() +
                "', '" + CurrentRegimen.Trim() +
                "', '" + Prescription.Trim() +
                "', CONVERT(datetime, '" + RecordedDate + "', 103)" +
                ", CONVERT(datetime, '" + UpdatedDate + "', 103)" +
                ")";
            DataTable dt = new DataTable();
            message = connFactory.createDataIntoDB(queryString);

            return message;
        }

        internal DataTable retrievePatientMedicalRecords(string patientId)
        {
            string queryString = "select consultationDate, assignedDoctor, doctorId,  chiefComplaint, diagnosis, prescription, medicalRecordId " +
                "from MedicalRecords where patientId = " + Convert.ToInt32(patientId) + " order by consultationDate desc, assignedDoctor asc";

            dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        internal DataTable retrievePatientMedicalRecordsFilterByDoctor(string patientId, int doctorId)
        {
            string queryString = " select consultationDate, assignedDoctor, doctorId, chiefComplaint, diagnosis, prescription, medicalRecordId" +
                " from MedicalRecords where patientId = "+ patientId + " and doctorId like '%" + doctorId+"%'" +
                " order by consultationDate desc, doctorId asc, assignedDoctor asc, medicalRecordId asc";

            dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }
        
        internal DataTable getPatientMedicalRecords(string medicalRecordsId)
        {
            string queryString = "SELECT * FROM MedicalRecords where medicalRecordId = " +Convert.ToInt32(medicalRecordsId);

            dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        internal string updatePatientMedicalRecords(string medRecordId)
        {
            string queryString = "SET DATEFORMAT dmy; UPDATE MedicalRecords SET " +
                "patientId = " + MedPatientId.Trim() +
               ", doctorId = " + MedDoctorId.Trim() +
               ", employeeId = (select employeeId from Doctor where doctorId = " + Convert.ToInt32(MedDoctorId.Trim()) + ")" +
               ", assignedDoctor = '" + MedAssignedDoctor.Trim() +
               "', consultationDate = CONVERT(datetime, '" + ConsultationDate + "', 103)" +
               ", bloodPressure = '" + BloodPressure.Trim() +
               "', bloodType = '" + BloodType.Trim() +
               "', weight = " + Weight +
               ", height = " + Height +
               ", familyHistory = '" + FamilyHistory.Trim() +
               "', pastHealthHistory = '" + PastHealthHistory.Trim() +
               "', isSmoking = '" + isSmoking.Trim() +
               "', isPregnant = '" + isPregnant.Trim() +
               "', hasDiabetes = '" + HasDiabetes.Trim() +
               "', hasHighBP = '" + HasHighBP.Trim() +
               "', hasHighCholesterol = '" + HasHighCholesterol.Trim() +
               "', hasHeartProblem = '" + HasHeartProblem.Trim() +
               "', hasAsthma = '" + HasAsthma.Trim() +
               "', hasEpilepsy = '" + hasEpilepsy.Trim() +
               "', hasPneumonia = '" + HasPneumonia.Trim() +
               "', hasHepatitis = '" + HasHepatitis.Trim() +
               "', hasUlcer = '" + HasUlcer.Trim() +
               "', hasLeukemia = '" + HasLeukemia.Trim() +
               "', hasTB = '" + HasTB.Trim() +
               "', hasHIV = '" + HasHIV.Trim() +
               "', chiefComplaint = '" + ChiefComplaint.Trim() +
               "', presentIllness = '" + PresentIllness.Trim() +
               "', medicationTaken = '" + MedicationTaken.Trim() +
               "', diagnosis = '" + Diagnosis.Trim() +
               "', currentRegimen = '" + CurrentRegimen.Trim() +
               "', prescription = '" + Prescription.Trim() +
               "', updatedDate = CONVERT(datetime, '" + UpdatedDate + "', 103) where medicalRecordId =" + Convert.ToInt32(medRecordId);

            DataTable dt = new DataTable();
            message = connFactory.updateDataIntoDB(queryString);

            return message;
        }

        internal string deleteMedicalRecord(string medicalRecordId)
        {
            string queryString = "delete from MedicalRecords where medicalRecordId =  " + Convert.ToInt32(medicalRecordId);

            message = connFactory.deleteDataFromDB(queryString, "Appointment");

            return message;

        }
    }
}
