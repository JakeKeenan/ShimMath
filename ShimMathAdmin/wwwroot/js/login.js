//var forge = require('node-forge');
var md = forge.md.sha1.create();
md.update('The quick brown fox jumps over the lazy dog');
console.log(md.digest().toHex())

//password is hashed on the client side in-order to prove
//ShimMath is not farming passwords. Further Secuirty measures
//are taken on the server side to protect the password (See: UserService.cs)
function HashPassword(unHashedPassword, publicSalt){
    var md = forge.md.sha256.create();
    md.update(unHashedPassword + publicSalt, 'utf8');
    hashedPassword = md.digest().toHex()
    //console.log(md.digest().toHex())
}

function SubmitPassword() {

}
