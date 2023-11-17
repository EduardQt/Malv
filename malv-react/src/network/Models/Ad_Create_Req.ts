

export interface Ad_Create_Req  {
    title: string;
    description: string;
    carAdMod?: string | null;
    categoryId: number;
    municipalityId: number;
    files: File[];
}