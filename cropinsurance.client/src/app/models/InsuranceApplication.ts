export interface InsuranceApplication {
    applicationId: number;
    seasonId: number;
    seasonName: string;
    cropId: number;
    cropName: string;
    farmerName: string;
    aadNo: string;
    completeAddress: string;
    farmerCategory: "Small" | "Medium" | "Large";
    submissionDate: Date;
}