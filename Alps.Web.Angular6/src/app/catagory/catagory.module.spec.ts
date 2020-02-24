import { CatagoryModule } from './catagory.module';

describe('CatagoryModule', () => {
  let catagoryModule: CatagoryModule;

  beforeEach(() => {
    catagoryModule = new CatagoryModule();
  });

  it('should create an instance', () => {
    expect(catagoryModule).toBeTruthy();
  });
});
