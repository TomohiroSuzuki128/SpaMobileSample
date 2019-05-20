var app = new Vue({
    el: '#app',
    data:{
        initialized: false,
        zipCode: '',
        prefecture: '',
        prefectureList:[],
        address1: '',
        address2: '',
        message: '',
        showMessage: false,
        },
    created: function(){
        axios.get("./Api/Prefectures")
            .then((response) => {
                this.prefectureList = response.data;
                this.prefecture = '1';
            });
        this.initialized = true;
    },
    methods:{
        handleZipCodeToAddressClick: function(event){
            this.showMessage = false;
            if (this.zipCode.match(/[^0-9]+/)) {
                this.message = '郵便番号は数字のみ入力してください。'
                this.showMessage = true;
                return;
            }
            if (this.zipCode.length != 7) {
                this.message = '郵便番号は7桁を入力してください。'
                this.showMessage = true;
                return;
            }
            axios.get("./Api/ZipCodeToAddress?q="+this.zipCode)
                .then((response) => {
                    if(response.data.length === 0){
                        this.message = '該当する情報が見つかりません。'
                        this.showMessage = true;
                        return;
                    }
                    if(response.data.length > 1){
                        this.message = '住所が特定できませんでした。'
                        this.showMessage = true;
                        return;
                    }
                    let address = response.data[0];
                    this.prefecture = this.prefectureList.filter(pref => pref.name == address.prefecture)[0].code;
                    this.address1 = address.city + address.machi;
                    this.address2 = '';
                });
        },
        handleAddressToZipCodeClick: function (event){
            this.showMessage = false;
            if (this.prefecture.length === 0 || this.address1.length === 0) {
                this.message = '都道府県と市区町村の両方を入力してください。'
                this.showMessage = true;
                return;
            }
            axios.get("./Api/AddressToZipCode?pref="+this.prefecture+"&addr="+this.address1)
                .then((response) => {
                    if(response.data.length === 0){
                        this.message = '該当する情報が見つかりません。'
                        this.showMessage = true;
                        return;
                    }
                    if(response.data.length > 1){
                        this.message = '住所が特定できませんでした。'
                        this.showMessage = true;
                        return;
                    }
                    let address = response.data[0];
                    this.zipCode = address.zipCode;
                });
        }
    }
});
