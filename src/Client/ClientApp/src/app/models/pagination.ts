export class pagination{
  constructor(public pageNumber: number,
              public pageSize: number,
              public totalCount: number) {
  }

  public paginated : boolean = this.totalCount > this.pageSize;
  public totalPages(): number{
    return Math.ceil(this.totalCount / this.pageSize);
  }
  public hasPrevious(): boolean {
    return this.pageNumber>1;
}
  public hasNext(): boolean {
    return this.pageNumber < this.totalPages();
  }
}
